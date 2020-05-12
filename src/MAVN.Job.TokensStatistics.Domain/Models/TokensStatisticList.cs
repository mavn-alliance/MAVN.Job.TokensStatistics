using System.Collections.Generic;
using MAVN.Numerics;

namespace MAVN.Job.TokensStatistics.Domain.Models
{
    public class TokensStatisticList
    {
        public IReadOnlyList<TokensStatistic> Earn { get; set; }

        public IReadOnlyList<TokensStatistic> Burn { get; set; }
        
        public IReadOnlyList<TokensStatistic> WalletBalance { get; set; }

        public Money18 TotalEarn { get; set; }

        public Money18 TotalBurn { get; set; }
        
        public Money18 TotalWalletBalance { get; set; }
        public Money18 TotalCustomersWalletBalance { get; set; }
    }
}
