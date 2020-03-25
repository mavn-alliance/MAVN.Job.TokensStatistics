using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.TokensStatistics.Settings.JobSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        public string MsSqlConnString { get; set; }
    }
}
