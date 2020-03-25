using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Job.TokensStatistics.Domain.Models;

namespace Lykke.Job.TokensStatistics.Domain.Repositories
{
    public interface ITokensSnapshotRepository
    {
        Task SaveTokensSnapshotAsync(DailyTokensSnapshot dailyTokensSnapshot);

        Task<DailyTokensSnapshot> GetTokensSnapshotByDate(string date);
        
        Task<IReadOnlyList<DailyTokensSnapshot>> GetTokensSnapshotsByPeriod(DateTime fromDate, DateTime toDate);
    }
}
