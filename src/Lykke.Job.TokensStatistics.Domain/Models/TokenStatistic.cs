using System;
using Falcon.Numerics;

namespace Lykke.Job.TokensStatistics.Domain.Models
{
    public class TokensStatistic
    {
        public DateTime Day { get; set; }

        public Money18 Amount { get; set; }
    }
}
