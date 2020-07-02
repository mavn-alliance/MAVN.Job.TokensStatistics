using Autofac;
using JetBrains.Annotations;
using MAVN.Job.TokensStatistics.Domain.Repositories;
using MAVN.Job.TokensStatistics.MsSqlRepositories;
using MAVN.Job.TokensStatistics.MsSqlRepositories.Repositories;
using MAVN.Job.TokensStatistics.Settings;
using Lykke.SettingsReader;
using MAVN.Persistence.PostgreSQL.Legacy;

namespace MAVN.Job.TokensStatistics.Modules
{
    [UsedImplicitly]
    public class DataLayerModule : Module
    {
        private readonly string _connectionString;

        public DataLayerModule(IReloadingManager<AppSettings> appSettings)
        {
            _connectionString = appSettings.CurrentValue.TokensStatisticsJob.Db.MsSqlConnString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterPostgreSQL(
                _connectionString,
                connString => new TokensStatisticsContext(connString, false),
                dbConn => new TokensStatisticsContext(dbConn));

            builder.RegisterType<TokensSnapshotRepository>()
                .As<ITokensSnapshotRepository>()
                .SingleInstance();

            builder.RegisterType<LastKnownStatsRepository>()
                .As<ILastKnownStatsRepository>()
                .SingleInstance();
        }
    }
}
