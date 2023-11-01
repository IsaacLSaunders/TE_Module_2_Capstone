using System.ComponentModel.DataAnnotations;

namespace TEBucksServer.Models
{
    public class Account
    {
        public const decimal INITIAL_BALANCE = 1000M;
        public int AccountId { get; set; }
        [Required]
        public int UserId { get; set; }
        public decimal Balance { get; set; } = INITIAL_BALANCE;
        
        public Account(int userId)
        {
            UserId = userId;
        }

        public Account() { }
    }

}
