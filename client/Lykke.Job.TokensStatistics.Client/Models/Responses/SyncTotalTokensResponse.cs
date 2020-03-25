using JetBrains.Annotations;
using Lykke.Job.TokensStatistics.Client.Enums;

namespace Lykke.Job.TokensStatistics.Client.Models.Responses
{
    /// <summary>
    /// Sync total tokens amount with blockchain response
    /// </summary>
    [PublicAPI]
    public class SyncTotalTokensResponse
    {
        /// <summary>
        /// Error code
        /// </summary>
        public TokensErrorCodes ErrorCode { get; set; }
    }
}
