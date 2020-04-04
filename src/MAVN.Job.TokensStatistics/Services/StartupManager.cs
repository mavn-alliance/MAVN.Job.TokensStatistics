using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common;
using Lykke.Common.Log;
using MAVN.Job.TokensStatistics.Domain.Services;
using Lykke.Sdk;

namespace MAVN.Job.TokensStatistics.Services
{
    public class StartupManager : IStartupManager
    {
        private readonly ITokensStatisticsService _tokensStatisticsService;
        private readonly IEnumerable<IStartStop> _startables;
        private readonly ILog _log;

        public StartupManager(
            ITokensStatisticsService tokensStatisticsService,
            IEnumerable<IStartStop> startables,
            ILogFactory logFactory)
        {
            _tokensStatisticsService = tokensStatisticsService;
            _startables = startables;
            _log = logFactory.CreateLog(this);
        }

        public async Task StartAsync()
        {
            _log.Info("Initializing tokens statistics.");
            await _tokensStatisticsService.Initialize();

            foreach (var startable in _startables)
            {
                startable.Start();
            }
        }
    }
}
