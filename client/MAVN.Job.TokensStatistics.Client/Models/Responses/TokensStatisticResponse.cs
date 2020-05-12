using System;
using JetBrains.Annotations;
using MAVN.Numerics;

namespace MAVN.Job.TokensStatistics.Client.Models.Responses
{
    /// <summary>
    /// Represents tokens statistic response model
    /// </summary>
    [PublicAPI]
    public class TokensStatisticResponse
    {
        /// <summary>
        /// The day
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// The amount of token for the day
        /// </summary>
        public Money18 Amount { get; set; }
    }
}
