using System;
using MAVN.Numerics;

namespace MAVN.Job.TokensStatistics.Domain.Models
{
    public class TokensStatistic
    {
        public DateTime Day { get; set; }

        public Money18 Amount { get; set; }
    }
}
