using System;
using System.Threading.Tasks;
using MAVN.Job.TokensStatistics.Domain.Enums;
using MAVN.Job.TokensStatistics.Domain.Models;
using MAVN.Job.TokensStatistics.Domain.Repositories;
using MAVN.Job.TokensStatistics.DomainServices.Services;
using Lykke.Logs;
using Lykke.Logs.Loggers.LykkeConsole;
using Lykke.Service.PrivateBlockchainFacade.Client;
using Lykke.Service.PrivateBlockchainFacade.Client.Models;
using Moq;
using Xunit;

namespace MAVN.Job.TokensStatistics.Tests
{
    // todo: update tests with new earned and burned statistics
    public class TokensStatisticsServiceTests
    {
        [Fact]
        public async Task TryToInitServiceTwice_InvalidOperation_ExceptionIsThrown()
        {
            var snapshotRepo = Mock.Of<ITokensSnapshotRepository>();
            var lastTokensTotalAmountRepo = Mock.Of<ILastKnownStatsRepository>();
            var pbfClient = Mock.Of<IPrivateBlockchainFacadeClient>();

            TokensStatisticsService tokensStatisticsService;
            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                tokensStatisticsService = new TokensStatisticsService(logFactory,
                    pbfClient,
                    snapshotRepo,
                    lastTokensTotalAmountRepo);
            }

            await tokensStatisticsService.Initialize();

            await Assert.ThrowsAsync<InvalidOperationException>(() => tokensStatisticsService.Initialize());
        }

        [Fact]
        public async Task TryToGetTokensSnapshot_SnapshotExists_ResultWithoutError()
        {
            var lastTokensTotalAmountRepo = Mock.Of<ILastKnownStatsRepository>();
            var pbfClient = Mock.Of<IPrivateBlockchainFacadeClient>();
            var snapshotRepo = new Mock<ITokensSnapshotRepository>();
            snapshotRepo.Setup(x => x.GetTokensSnapshotByDate(It.IsAny<string>()))
                .ReturnsAsync(new DailyTokensSnapshot());

            TokensStatisticsService tokensStatisticsService;
            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                tokensStatisticsService = new TokensStatisticsService(logFactory,
                    pbfClient,
                    snapshotRepo.Object,
                    lastTokensTotalAmountRepo);
            }

            var result = await tokensStatisticsService.GetTokensSnapshotForDate(DateTime.UtcNow);

            Assert.Equal(TokensErrorCodes.None, result.ErrorCode);
        }

        [Fact]
        public async Task TryToGetTokensSnapshot_SnapshotDoesNotExists_ResultWithBusinessError()
        {
            var lastTokensTotalAmountRepo = Mock.Of<ILastKnownStatsRepository>();
            var pbfClient = Mock.Of<IPrivateBlockchainFacadeClient>();
            var snapshotRepo = new Mock<ITokensSnapshotRepository>();
            snapshotRepo.Setup(x => x.GetTokensSnapshotByDate(It.IsAny<string>()))
                .ReturnsAsync((DailyTokensSnapshot)null);

            TokensStatisticsService tokensStatisticsService;
            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                tokensStatisticsService = new TokensStatisticsService(logFactory,
                    pbfClient,
                    snapshotRepo.Object,
                    lastTokensTotalAmountRepo);
            }

            var result = await tokensStatisticsService.GetTokensSnapshotForDate(DateTime.UtcNow.AddDays(-1));

            Assert.Equal(TokensErrorCodes.StatisticsNotFound, result.ErrorCode);
        }
        
        [Fact]
        public async Task TryToSaveSnapshot_EverythingValid_SnapshotIsSaved()
        {
            var lastTokensTotalAmountRepo = Mock.Of<ILastKnownStatsRepository>();
            var pbfClient = new Mock<IPrivateBlockchainFacadeClient>();
            pbfClient.Setup(x => x.TokensApi.GetTotalTokensSupplyAsync())
                .ReturnsAsync(new TotalTokensSupplyResponse());

            var snapshotRepo = new Mock<ITokensSnapshotRepository>();
            snapshotRepo.Setup(x => x.SaveTokensSnapshotAsync(It.IsAny<DailyTokensSnapshot>()))
                .Verifiable();


            TokensStatisticsService tokensStatisticsService;
            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                tokensStatisticsService = new TokensStatisticsService(logFactory,
                    pbfClient.Object,
                    snapshotRepo.Object,
                    lastTokensTotalAmountRepo);
            }

            await tokensStatisticsService.SaveTokensSnapshotAsync();

            snapshotRepo.Verify();
        }

        [Fact]
        public async Task TryToSyncTotalTokensWithPbf_PbfIsAvailable_SuccessfullySynced()
        {
            var snapshotRepo = Mock.Of<ITokensSnapshotRepository>();
            var lastTokensTotalAmountRepo = Mock.Of<ILastKnownStatsRepository>();
            var pbfClient = new Mock<IPrivateBlockchainFacadeClient>();
            pbfClient.Setup(x => x.TokensApi.GetTotalTokensSupplyAsync())
                .ReturnsAsync(new TotalTokensSupplyResponse());
            pbfClient.Setup(x => x.TokensApi.GetTokenGatewayWalletBalance())
                .ReturnsAsync(new TotalTokensSupplyResponse());

            TokensStatisticsService tokensStatisticsService;
            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                tokensStatisticsService = new TokensStatisticsService(logFactory,
                    pbfClient.Object,
                    snapshotRepo,
                    lastTokensTotalAmountRepo);
            }

            var result = await tokensStatisticsService.SyncTotalTokensAsync();

            Assert.Equal(TokensErrorCodes.None, result);
        }

        [Fact]
        public async Task TryToSyncTotalTokensWithPbf_PbfNotAvailable_BusinessErrorIsReturned()
        {
            var snapshotRepo = Mock.Of<ITokensSnapshotRepository>();
            var lastTokensTotalAmountRepo = Mock.Of<ILastKnownStatsRepository>();
            var pbfClient = new Mock<IPrivateBlockchainFacadeClient>();
            pbfClient.Setup(x => x.TokensApi.GetTotalTokensSupplyAsync())
                .ThrowsAsync(new Exception());


            TokensStatisticsService tokensStatisticsService;
            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                tokensStatisticsService = new TokensStatisticsService(logFactory,
                    pbfClient.Object,
                    snapshotRepo,
                    lastTokensTotalAmountRepo);
            }

            var result = await tokensStatisticsService.SyncTotalTokensAsync();

            Assert.Equal(TokensErrorCodes.PrivateBlockchainFacadeIsNotAvailable, result);
        }
    }
}
