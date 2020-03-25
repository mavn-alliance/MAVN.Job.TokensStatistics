using System;
using Falcon.Numerics;

namespace Lykke.Job.TokensStatistics.Domain.Models
{
    public interface ILastKnownStats
    {
        Money18 LastTotalAmount { get; set; }

        Money18 LastEarnedAmount { get; set; }

        Money18 LastBurnedAmount { get; set; }

        DateTime Timestamp { get; set; }

        Money18 LastTotalTokensInCustomersWallets { get; set; }
    }
}
