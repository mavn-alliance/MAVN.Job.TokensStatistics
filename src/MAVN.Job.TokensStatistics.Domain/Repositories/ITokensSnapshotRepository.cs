using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Job.TokensStatistics.Domain.Models;

namespace MAVN.Job.TokensStatistics.Domain.Repositories
{
    public interface ITokensSnapshotRepository
    {
        Task SaveTokensSnapshotAsync(DailyTokensSnapshot dailyTokensSnapshot);

        Task<DailyTokensSnapshot> GetTokensSnapshotByDate(string date);
        
        Task<IReadOnlyList<DailyTokensSnapshot>> GetTokensSnapshotsByPeriod(DateTime fromDate, DateTime toDate);
    }
}
