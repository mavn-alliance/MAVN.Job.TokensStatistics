using System;

namespace Lykke.Job.TokensStatistics.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToTokensStatisticsDateString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
