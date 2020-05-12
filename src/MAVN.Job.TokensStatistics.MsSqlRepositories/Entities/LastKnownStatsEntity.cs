using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Job.TokensStatistics.Domain.Models;
using MAVN.Numerics;

namespace MAVN.Job.TokensStatistics.MsSqlRepositories.Entities
{
    [Table("last_token_total_amount")]
    public class LastKnownStatsEntity : ILastKnownStats
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public long Id { get; set; }

        [Column("last_total_amount")]
        [Required]
        public Money18 LastTotalAmount { get; set; }

        [Column("last_total_tokens_in_customers_wallets")]
        [Required]
        public Money18 LastTotalTokensInCustomersWallets { get; set; }

        [Column("last_earned_amount")]
        [Required]
        public Money18 LastEarnedAmount { get; set; }
        
        [Column("last_burned_amount")]
        [Required]
        public Money18 LastBurnedAmount { get; set; }

        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        public static LastKnownStatsEntity Create(Money18 lastTotalAmount, Money18 lastEarnedAmount, Money18 lastBurnedAmount, Money18 lastTotalInCustomersWallets)
        {
            return new LastKnownStatsEntity
            {
                LastTotalAmount = lastTotalAmount,
                Timestamp = DateTime.UtcNow,
                LastBurnedAmount = lastBurnedAmount,
                LastEarnedAmount = lastEarnedAmount,
                LastTotalTokensInCustomersWallets = lastTotalInCustomersWallets,
            };
        }
    }
}
