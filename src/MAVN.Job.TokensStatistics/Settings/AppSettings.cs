using MAVN.Job.TokensStatistics.Settings.JobSettings;
using Lykke.Sdk.Settings;
using Lykke.Service.PrivateBlockchainFacade.Client;

namespace MAVN.Job.TokensStatistics.Settings
{
    public class AppSettings : BaseAppSettings
    {
        public TokensStatisticsJobSettings TokensStatisticsJob { get; set; }

        public PrivateBlockchainFacadeServiceClientSettings PrivateBlockchainFacadeService { get; set; }
    }
}
