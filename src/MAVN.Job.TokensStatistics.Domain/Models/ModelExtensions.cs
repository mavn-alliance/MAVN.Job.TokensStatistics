using System;
using System.Collections.Generic;
using System.Linq;
using Falcon.Numerics;
using Falcon.Numerics.Linq;

namespace MAVN.Job.TokensStatistics.Domain.Models
{
    public static class ModelExtensions
    {
        public static TokensStatistic ToEarnModel(this DailyTokensSnapshot src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new TokensStatistic { Day = DateTime.Parse(src.Date), Amount = src.TotalEarnedTokensAmount };
        }

        public static TokensStatistic ToBurnModel(this DailyTokensSnapshot src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new TokensStatistic { Day = DateTime.Parse(src.Date), Amount = src.TotalBurnedTokensAmount };
        }

        public static TokensStatistic ToBalanceModel(this DailyTokensSnapshot src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new TokensStatistic { Day = DateTime.Parse(src.Date), Amount = src.TotalTokensAmount };
        }

        public static TokensStatisticList ToStatisticModel(this IReadOnlyList<DailyTokensSnapshot> src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            var lastDayInfo = src.LastOrDefault();

            return new TokensStatisticList
            {
                Earn = src.Select(d => d.ToEarnModel()).ToList(),
                Burn = src.Select(d => d.ToBurnModel()).ToList(),
                WalletBalance = src.Select(d => d.ToBalanceModel()).ToList(),
                TotalEarn = src.Sum(d => d.TotalEarnedTokensAmount),
                TotalBurn = src.Sum(d => d.TotalBurnedTokensAmount),
                TotalWalletBalance = lastDayInfo?.TotalTokensAmount ?? 0,
                TotalCustomersWalletBalance = lastDayInfo?.TotalTokensInCustomersWallets ?? 0,
            };
        }
    }
}
