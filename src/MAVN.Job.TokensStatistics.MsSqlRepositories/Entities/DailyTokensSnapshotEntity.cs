using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Job.TokensStatistics.Domain.Models;
using Falcon.Numerics;

namespace MAVN.Job.TokensStatistics.MsSqlRepositories.Entities
{
    [Table("daily_tokens_snapshot")]
    public class DailyTokensSnapshotEntity
    {
        [Key]
        [Column("date")]
        [Required]
        public string Date { get; set; }

        [Column("total_tokens")]
        [Required]
        public Money18 TotalTokens { get; set; }

        [Column("total_tokens_in_customers_wallets")]
        [Required]
        public Money18 TotalTokensInCustomersWallets { get; set; }

        [Column("total_earned_tokens")]
        [Required]
        public Money18 TotalEarnedTokens { get; set; }
        
        [Column("total_burned_tokens")]
        [Required]
        public Money18 TotalBurnedTokens { get; set; }

        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        public static DailyTokensSnapshotEntity Create(DailyTokensSnapshot dailyTokensSnapshot)
        {
            return new DailyTokensSnapshotEntity
            {
                Date = dailyTokensSnapshot.Date,
                TotalTokens = dailyTokensSnapshot.TotalTokensAmount,
                Timestamp = dailyTokensSnapshot.Timestamp,
                TotalEarnedTokens = dailyTokensSnapshot.TotalEarnedTokensAmount,
                TotalBurnedTokens = dailyTokensSnapshot.TotalBurnedTokensAmount,
                TotalTokensInCustomersWallets = dailyTokensSnapshot.TotalTokensInCustomersWallets,
            };
        }
    }
}
