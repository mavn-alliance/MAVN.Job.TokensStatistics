using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common;
using Lykke.Common.Log;
using Lykke.Job.TokensStatistics.Domain.Services;
using Lykke.Sdk;

namespace Lykke.Job.TokensStatistics.Services
{
    public class ShutdownManager : IShutdownManager
    {
        private readonly ILog _log;
        private readonly IEnumerable<IStartStop> _stoppables;
        private readonly IEnumerable<IStopable> _items;
        private readonly ITokensStatisticsService _tokensStatisticsService;

        public ShutdownManager(
            ILogFactory logFactory,
            IEnumerable<IStartStop> stoppables,
            IEnumerable<IStopable> items,
            ITokensStatisticsService tokensStatisticsService)
        {
            _log = logFactory.CreateLog(this);
            _stoppables = stoppables;
            _items = items;
            _tokensStatisticsService = tokensStatisticsService;
        }

        public async Task StopAsync()
        {
            _log.Info("Saving last tokens total stats on shutdown");
            await _tokensStatisticsService.SaveLastKnownStatsAsync();

            try
            {
                await Task.WhenAll(_stoppables.Select(i => Task.Run(() => i.Stop())));

                await Task.WhenAll(_items.Select(i => Task.Run(() => i.Stop())));
            }
            catch (Exception ex)
            {
                _log.Warning("Unable to stop a component", ex);
            }
        }
    }
}
