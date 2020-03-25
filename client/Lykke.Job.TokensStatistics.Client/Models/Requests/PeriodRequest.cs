using System;
using JetBrains.Annotations;

namespace Lykke.Job.TokensStatistics.Client.Models.Requests
{
    /// <summary>
    /// Represents a base period request model
    /// </summary>
    [PublicAPI]
    public class PeriodRequest
    {
        /// <summary>
        ///  Represents FromDate 
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        ///  Represents ToDate
        /// </summary>
        public DateTime ToDate { get; set; }
    }
}
