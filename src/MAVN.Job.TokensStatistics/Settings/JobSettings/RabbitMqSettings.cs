using Lykke.SettingsReader.Attributes;

namespace MAVN.Job.TokensStatistics.Settings.JobSettings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
