using System.ComponentModel.DataAnnotations;

namespace TEBucksServer.Models
{
    public class Account
    {
        public const decimal INITIAL_BALANCE = 1000M;
        public int AccountId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Balance { get; set; } = INITIAL_BALANCE;
    }
}
