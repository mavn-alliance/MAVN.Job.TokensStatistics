using JetBrains.Annotations;
using Lykke.Job.TokensStatistics.Client.Models.Requests;
using Lykke.Job.TokensStatistics.Client.Models.Responses;
using Refit;
using System;
using System.Threading.Tasks;

namespace Lykke.Job.TokensStatistics.Client
{
    // This is an example of service controller interfaces.
    // Actual interface methods must be placed here (not in ITokensStatisticsClient interface).

    /// <summary>
    /// TokensStatistics client API interface.
    /// </summary>
    [PublicAPI]
    public interface ITokensStatisticsApi
    {
        /// <summary>
        /// Gets total amount of tokens in the system
        /// </summary>
        /// <returns><see cref="TotalTokensResponse"/></returns>
        [Get("/api/general/total")]
        Task<TotalTokensResponse> GetTotalAmountAsync(DateTime dt);

        /// <summary>
        /// Get tokens statistics for period grouped by days
        /// </summary>
        /// <param name="request">The period details</param>
        /// <returns></returns>
        [Get("/api/general/byDays")]
        Task<TokensStatisticListResponse> GetByDaysAsync([Query] PeriodRequest request);
    }
}
