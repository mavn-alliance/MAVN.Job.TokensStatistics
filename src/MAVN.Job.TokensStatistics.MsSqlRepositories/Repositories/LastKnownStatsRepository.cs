using System;
using System.Threading.Tasks;
using MAVN.Job.TokensStatistics.Domain.Models;
using MAVN.Job.TokensStatistics.Domain.Repositories;
using MAVN.Job.TokensStatistics.MsSqlRepositories.Entities;
using MAVN.Numerics;
using MAVN.Persistence.PostgreSQL.Legacy;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Job.TokensStatistics.MsSqlRepositories.Repositories
{
    public class LastKnownStatsRepository : ILastKnownStatsRepository
    {
        private readonly PostgreSQLContextFactory<TokensStatisticsContext> _contextFactory;

        public LastKnownStatsRepository(PostgreSQLContextFactory<TokensStatisticsContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ILastKnownStats> GetLastKnownStatsAsync()
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.LastKnownStats.FirstOrDefaultAsync();

                return result;
            }
        }

        public async Task UpsertLastKnownStatsAsync(Money18 totalAmount, Money18 totalEarnedAmount, Money18 totalBurnedAmount, Money18 totalInCustomersWallets)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var existingEntity = await context.LastKnownStats.FirstOrDefaultAsync();

                if (existingEntity != null)
                {
                    existingEntity.LastTotalAmount = totalAmount;
                    existingEntity.LastBurnedAmount = totalBurnedAmount;
                    existingEntity.LastEarnedAmount = totalEarnedAmount;
                    existingEntity.LastTotalTokensInCustomersWallets = totalInCustomersWallets;
                    existingEntity.Timestamp = DateTime.UtcNow;
                    context.Update(existingEntity);
                }
                else
                {
                    var newEntity = LastKnownStatsEntity.Create(totalAmount, totalEarnedAmount, totalBurnedAmount, totalInCustomersWallets);
                    context.Add(newEntity);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
