using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Job.TokensStatistics.Domain.Enums;
using MAVN.Job.TokensStatistics.Domain.Extensions;
using MAVN.Job.TokensStatistics.Domain.Models;
using MAVN.Job.TokensStatistics.Domain.Repositories;
using MAVN.Job.TokensStatistics.Domain.Services;
using MAVN.Numerics;
using MAVN.Service.PrivateBlockchainFacade.Client;
using MAVN.Service.PrivateBlockchainFacade.Client.Models;
using Polly;

namespace MAVN.Job.TokensStatistics.DomainServices.Services
{
    public class TokensStatisticsService : ITokensStatisticsService
    {
        private readonly IPrivateBlockchainFacadeClient _pbfClient;
        private readonly ITokensSnapshotRepository _tokensSnapshotRepository;
        private readonly ILastKnownStatsRepository _lastKnownStatsRepository;
        private readonly ILog _log;
        private bool _isInitialized;
        private readonly object _locker = new object();

        private Money18 _totalTokensAmount;
        private Money18 _todaysEarnedAmount;
        private Money18 _todaysBurnedAmount;
        private Money18 _totalTokensInCustomersWallets;

        public TokensStatisticsService(ILogFactory logFactory,
            IPrivateBlockchainFacadeClient pbfClient,
            ITokensSnapshotRepository tokensSnapshotRepository,
            ILastKnownStatsRepository lastKnownStatsRepository)
        {
            _pbfClient = pbfClient;
            _tokensSnapshotRepository = tokensSnapshotRepository;
            _lastKnownStatsRepository = lastKnownStatsRepository;
            _log = logFactory.CreateLog(this);
        }
        public async Task Initialize()
        {
            if (_isInitialized)
                throw new InvalidOperationException("Service was already initialized");

            var lastTotalAmountInDb = await _lastKnownStatsRepository.GetLastKnownStatsAsync();

            _totalTokensAmount = lastTotalAmountInDb?.LastTotalAmount ?? 0;
            _totalTokensInCustomersWallets = lastTotalAmountInDb?.LastTotalTokensInCustomersWallets ?? 0;

            var lastSavedToday = lastTotalAmountInDb?.Timestamp.Date == DateTime.UtcNow.Date;
            _todaysEarnedAmount = lastSavedToday ? lastTotalAmountInDb.LastEarnedAmount : 0;
            _todaysBurnedAmount = lastSavedToday ? lastTotalAmountInDb.LastBurnedAmount : 0;

            _isInitialized = true;
        }

        public async Task<DailyTokensSnapshot> GetTokensSnapshotForDate(DateTime date)
        {
            var today = DateTime.UtcNow.Date;
            if (date.Date == today)
            {
                var syncError = await SyncTotalTokensAsync();

                if (syncError != TokensErrorCodes.None)
                {
                    _log.Warning("Couldn't sync total tokens supply with PBF", context: syncError.ToString());
                }

                return DailyTokensSnapshot.Create
                (today, _totalTokensAmount, _totalTokensInCustomersWallets, _todaysEarnedAmount,
                    _todaysBurnedAmount, DateTime.UtcNow);
            }

            var dateAsString = date.ToTokensStatisticsDateString();

            var result = await _tokensSnapshotRepository.GetTokensSnapshotByDate(dateAsString);

            if (result == null)
                result = new DailyTokensSnapshot { ErrorCode = TokensErrorCodes.StatisticsNotFound };

            return result;
        }

        public async Task<TokensStatisticList> GetTokensStatsForPeriod(DateTime fromDate, DateTime toDate)
        {
            var snapshots = await _tokensSnapshotRepository.GetTokensSnapshotsByPeriod(fromDate, toDate);

            var today = DateTime.UtcNow.Date;
            if (toDate.Date == today && snapshots.All(s => s.Date != today.ToTokensStatisticsDateString()))
            {
                var todayTokensHistory = await GetTokensSnapshotForDate(toDate);

                var extendedSnapshots = new List<DailyTokensSnapshot>(snapshots) { todayTokensHistory };

                snapshots = extendedSnapshots;
            }

            return snapshots.ToStatisticModel();
        }

        public async Task SaveTokensSnapshotAsync()
        {
            var now = DateTime.UtcNow;
            var dateOfSnapshot = now.Date;

            //This check if the save started after midnight, if not do not subtract one day
            if (now.Hour < 12)
                dateOfSnapshot = dateOfSnapshot.AddDays(-1);

            _log.Info($"Started the snapshot procedure of total tokens statistics in the system for {dateOfSnapshot}");

            var syncError = await SyncTotalTokensAsync();

            if (syncError != TokensErrorCodes.None)
            {
                _log.Warning("Couldn't sync total tokens supply with PBF", context: syncError.ToString());
            }

            try
            {
                var timestampOfSnapshot = DateTime.UtcNow;

                var dailyTokensHistory = DailyTokensSnapshot.Create
                (dateOfSnapshot, _totalTokensAmount, _totalTokensInCustomersWallets, _todaysEarnedAmount,
                    _todaysBurnedAmount, timestampOfSnapshot);

                await _tokensSnapshotRepository.SaveTokensSnapshotAsync(dailyTokensHistory);

                _todaysBurnedAmount = _todaysEarnedAmount = 0;
            }
            catch (Exception e)
            {
                _log.Error(e, "Exception occured during daily snapshot of total tokens amount");
            }

            _log.Info($"Finished the snapshot procedure of total tokens in the system for {dateOfSnapshot}");
        }

        public async Task SaveLastKnownStatsAsync()
        {
            await _lastKnownStatsRepository.UpsertLastKnownStatsAsync(_totalTokensAmount, _todaysEarnedAmount, _todaysBurnedAmount, _totalTokensInCustomersWallets);
        }

        public async Task<TokensErrorCodes> SyncTotalTokensAsync()
        {
            try
            {
                var totalTokensFromPbf = (await GetTotalTokensAsync()).TotalTokensAmount;
                var totalTokensOnTokenGateway = (await GetTotalTokensOnTokenGatewayAsync()).TotalTokensAmount;
                var totalTokensOnCustomersWallets = totalTokensFromPbf - totalTokensOnTokenGateway;

                _log.Info($@"Manually synchronized Total Tokens from PBF service. Old value: {_totalTokensAmount},
                            new value: {totalTokensFromPbf}");

                _totalTokensAmount = totalTokensFromPbf;

                _log.Info($@"Manually synchronized Total Tokens on customer wallets from PBF service. Old value: {_totalTokensInCustomersWallets},
                            new value: {totalTokensOnCustomersWallets}");

                _totalTokensInCustomersWallets = totalTokensOnCustomersWallets;

                return TokensErrorCodes.None;
            }
            catch
            {
                _log.Warning("Not able to get total tokens amount from pbf to Sync value");
                return TokensErrorCodes.PrivateBlockchainFacadeIsNotAvailable;
            }
        }

        public Money18 GetCurrentTotalAmountAsync()
        {
            return _totalTokensAmount;
        }

        public void IncreaseEarnedAmount(Money18 amount)
        {
            lock (_locker)
            {
                _todaysEarnedAmount += amount;
            }
        }

        public void IncreaseBurnedAmount(Money18 amount)
        {
            lock (_locker)
            {
                _todaysBurnedAmount += amount;
            }
        }

        private Task<TotalTokensSupplyResponse> GetTotalTokensAsync()
        {
            return Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (ex, _) => _log.Error("Getting total tokens with retry", ex))
                .ExecuteAsync(_pbfClient.TokensApi.GetTotalTokensSupplyAsync);
        }

        private Task<TotalTokensSupplyResponse> GetTotalTokensOnTokenGatewayAsync()
        {
            return Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (ex, _) => _log.Error("Getting total tokens on token Gateway with retry", ex))
                .ExecuteAsync(_pbfClient.TokensApi.GetTokenGatewayWalletBalance);
        }

    }
}
