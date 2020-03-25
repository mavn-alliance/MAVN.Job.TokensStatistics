using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common;
using Lykke.Common.Log;
using Lykke.Job.TokensStatistics.Domain.Services;

namespace Lykke.Job.TokensStatistics.PeriodicalHandlers
{
    public class StoreTotalTokensPeriodicalHandler : IStartStop
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        private readonly TimerTrigger _timerTrigger;
        private readonly ILog _log;
        private readonly ITokensStatisticsService _tokensStatisticsService;

        public StoreTotalTokensPeriodicalHandler(ITokensStatisticsService tokensStatisticsService,
            ILogFactory logFactory)
        {
            _log = logFactory.CreateLog(this);
            _tokensStatisticsService = tokensStatisticsService;
            _timerTrigger = new TimerTrigger(nameof(StoreTotalTokensPeriodicalHandler), TimeSpan.FromDays(1), logFactory);
            _timerTrigger.Triggered += Execute;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            var timeUntilMidnight = DateTime.UtcNow.Date.AddDays(1) - DateTime.UtcNow;
            _log.Info($"Total tokens periodical handler will start at {DateTime.UtcNow.Add(timeUntilMidnight)}");

            Task.Delay(timeUntilMidnight, _cancellationToken)
                .ContinueWith((t) =>
                {
                    if (!t.IsCanceled)
                        _timerTrigger.Start();
                });
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _timerTrigger.Stop();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _timerTrigger.Stop();
            _timerTrigger.Dispose();
        }

        private Task Execute(ITimerTrigger timer, TimerTriggeredHandlerArgs args, CancellationToken cancellationToken)
        {
            // todo: if restart happened during handler activation time we will never get statistics snapshot for the last day
            Task.Run(async () => await _tokensStatisticsService.SaveTokensSnapshotAsync());
            return Task.CompletedTask;
        }
    }
}
