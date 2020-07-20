using System.Data.Common;
using JetBrains.Annotations;
using MAVN.Job.TokensStatistics.MsSqlRepositories.Entities;
using MAVN.Persistence.PostgreSQL.Legacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MAVN.Job.TokensStatistics.MsSqlRepositories
{
    public class TokensStatisticsContext : PostgreSQLContext
    {
        private const string Schema = "tokens_statistics";

        internal DbSet<DailyTokensSnapshotEntity> DailyTokensSnapshots { get; set; }
        internal DbSet<LastKnownStatsEntity> LastKnownStats { get; set; }

        /// <summary>Used by EF migrtions</summary>
        [UsedImplicitly]
        public TokensStatisticsContext() : base(Schema)
        {
        }

        public TokensStatisticsContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public TokensStatisticsContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        protected override void OnMAVNConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(cfg => cfg.Ignore(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnMAVNModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
