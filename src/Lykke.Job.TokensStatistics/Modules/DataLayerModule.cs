using Autofac;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using Lykke.Job.TokensStatistics.Domain.Repositories;
using Lykke.Job.TokensStatistics.MsSqlRepositories;
using Lykke.Job.TokensStatistics.MsSqlRepositories.Repositories;
using Lykke.Job.TokensStatistics.Settings;
using Lykke.SettingsReader;

namespace Lykke.Job.TokensStatistics.Modules
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
            builder.RegisterMsSql(
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
