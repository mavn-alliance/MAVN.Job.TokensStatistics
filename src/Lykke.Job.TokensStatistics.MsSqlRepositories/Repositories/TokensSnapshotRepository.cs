using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Job.TokensStatistics.Domain.Models;
using Lykke.Job.TokensStatistics.Domain.Repositories;
using Lykke.Job.TokensStatistics.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Job.TokensStatistics.MsSqlRepositories.Repositories
{
    public class TokensSnapshotRepository : ITokensSnapshotRepository
    {
        private readonly MsSqlContextFactory<TokensStatisticsContext> _contextFactory;

        public TokensSnapshotRepository(MsSqlContextFactory<TokensStatisticsContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task SaveTokensSnapshotAsync(DailyTokensSnapshot dailyTokensSnapshot)
        {
            var entity = DailyTokensSnapshotEntity.Create(dailyTokensSnapshot);

            using (var context = _contextFactory.CreateDataContext())
            {
                context.DailyTokensSnapshots.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<DailyTokensSnapshot> GetTokensSnapshotByDate(string date)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.DailyTokensSnapshots
                    .Where(s => s.Date == date)
                    .Select(s => new DailyTokensSnapshot
                    {
                        TotalTokensAmount = s.TotalTokens,
                        Date = s.Date,
                        Timestamp = s.Timestamp,
                        TotalEarnedTokensAmount = s.TotalEarnedTokens,
                        TotalBurnedTokensAmount = s.TotalBurnedTokens,
                        TotalTokensInCustomersWallets = s.TotalTokensInCustomersWallets,
                    })
                    .FirstOrDefaultAsync();

                return result;
            }
        }

        public async Task<IReadOnlyList<DailyTokensSnapshot>> GetTokensSnapshotsByPeriod(DateTime fromDate, DateTime toDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.DailyTokensSnapshots
                    .Where(s => DateTime.Parse(s.Date) >= fromDate && DateTime.Parse(s.Date) <= toDate)
                    .Select(s => new DailyTokensSnapshot
                    {
                        TotalTokensAmount = s.TotalTokens,
                        Date = s.Date,
                        Timestamp = s.Timestamp,
                        TotalEarnedTokensAmount = s.TotalEarnedTokens,
                        TotalBurnedTokensAmount = s.TotalBurnedTokens,
                        TotalTokensInCustomersWallets = s.TotalTokensInCustomersWallets,
                    })
                    .ToListAsync();

                return result;
            }
        }
    }
}
