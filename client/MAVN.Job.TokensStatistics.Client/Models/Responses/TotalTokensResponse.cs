using JetBrains.Annotations;
using MAVN.Job.TokensStatistics.Client.Enums;
using Falcon.Numerics;

namespace MAVN.Job.TokensStatistics.Client.Models.Responses
{
    /// <summary>
    /// Total tokens amount response
    /// </summary>
    [PublicAPI]
    public class TotalTokensResponse
    {
        /// <summary>
        /// Total amount of tokens
        /// </summary>
        public Money18 TotalTokensAmount { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public TokensErrorCodes ErrorCode { get; set; }
    }
}
