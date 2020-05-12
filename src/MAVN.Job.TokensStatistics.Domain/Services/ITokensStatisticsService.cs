using System;
using System.Threading.Tasks;
using MAVN.Job.TokensStatistics.Domain.Enums;
using MAVN.Job.TokensStatistics.Domain.Models;
using MAVN.Numerics;

namespace MAVN.Job.TokensStatistics.Domain.Services
{
    public interface ITokensStatisticsService
    {
        Task<DailyTokensSnapshot> GetTokensSnapshotForDate(DateTime date);
        
        Task<TokensStatisticList> GetTokensStatsForPeriod(DateTime fromDate, DateTime toDate);

        Task SaveTokensSnapshotAsync();

        Task Initialize();

        Task SaveLastKnownStatsAsync();

        Task<TokensErrorCodes> SyncTotalTokensAsync();

        Money18 GetCurrentTotalAmountAsync();

        void IncreaseEarnedAmount(Money18 amount);
        
        void IncreaseBurnedAmount(Money18 amount);
    }
}
