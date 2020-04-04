namespace MAVN.Job.TokensStatistics.Settings.JobSettings
{
    public class TokensStatisticsJobSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        public long TokenDeviationTolerance { get; set; }
    }
}
