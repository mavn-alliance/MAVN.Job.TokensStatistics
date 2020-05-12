using System.Threading.Tasks;
using MAVN.Job.TokensStatistics.Domain.Models;
using MAVN.Numerics;

namespace MAVN.Job.TokensStatistics.Domain.Repositories
{
    public interface ILastKnownStatsRepository
    {
        Task<ILastKnownStats> GetLastKnownStatsAsync();

        Task UpsertLastKnownStatsAsync(Money18 totalAmount, Money18 totalEarnedAmount, Money18 totalBurnedAmount, Money18 totalInCustomersWallets);
    }
}
