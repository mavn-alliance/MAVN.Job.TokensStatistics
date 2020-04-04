using System;
using MAVN.Job.TokensStatistics.Domain.Enums;
using MAVN.Job.TokensStatistics.Domain.Extensions;
using Falcon.Numerics;

namespace MAVN.Job.TokensStatistics.Domain.Models
{
    public class DailyTokensSnapshot
    {
        public string Date { get; set; }

        public Money18 TotalTokensAmount { get; set; }

        public Money18 TotalTokensInCustomersWallets { get; set; }

        public Money18 TotalEarnedTokensAmount { get; set; }
        
        public Money18 TotalBurnedTokensAmount { get; set; }

        public DateTime Timestamp { get; set; }

        public TokensErrorCodes ErrorCode { get; set; }

        public static DailyTokensSnapshot Create(DateTime date, Money18 totalTokensAmount,Money18 totalInCustomersWallets, Money18 totalEarned, Money18 totalBurned, DateTime timestampOfSnapshot)
        {
            return new DailyTokensSnapshot
            {
                Date = date.ToTokensStatisticsDateString(),
                TotalTokensAmount = totalTokensAmount,
                Timestamp = timestampOfSnapshot,
                TotalEarnedTokensAmount = totalEarned,
                TotalBurnedTokensAmount = totalBurned,
                TotalTokensInCustomersWallets = totalInCustomersWallets,
            };
        }
    }
}
