using System.Collections.Generic;
using JetBrains.Annotations;
using Falcon.Numerics;

namespace Lykke.Job.TokensStatistics.Client.Models.Responses
{
    /// <summary>
    /// Represents a response model for a tokens statistics list
    /// </summary>
    [PublicAPI]
    public class TokensStatisticListResponse
    {
        /// <summary>
        /// List of Earn Token separated by day
        /// </summary>
        public IReadOnlyList<TokensStatisticResponse> Earn { get; set; }

        /// <summary>
        /// List of Burn Token separated by day
        /// </summary>
        public IReadOnlyList<TokensStatisticResponse> Burn { get; set; }
        
        /// <summary>
        /// List of total wallets balance separated by day 
        /// </summary>
        public IReadOnlyList<TokensStatisticResponse> WalletBalance { get; set; }

        /// <summary>
        /// Total number of earn tokens
        /// </summary>
        public Money18 TotalEarn { get; set; }

        /// <summary>
        /// Total number of burn tokens
        /// </summary>
        public Money18 TotalBurn { get; set; }
        
        /// <summary>
        /// Total amount of tokens on all wallets for the last day of the period 
        /// </summary>
        public Money18 TotalWalletBalance { get; set; }

        /// <summary>
        /// Total amount of tokens on customers' wallets for the last day of the period 
        /// </summary>
        public Money18 TotalCustomersWalletBalance { get; set; }
    }
}
