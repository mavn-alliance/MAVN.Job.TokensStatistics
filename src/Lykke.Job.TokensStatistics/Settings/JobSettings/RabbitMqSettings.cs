using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.TokensStatistics.Settings.JobSettings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
