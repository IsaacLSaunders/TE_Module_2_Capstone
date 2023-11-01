using System.ComponentModel.DataAnnotations;

namespace TEBucksServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        [Required]
        public int UserFromId { get; set; }
        [Required]
        public int UserToId { get; set; }
        [Required]
        public string TransferType { get; set; }
        [Required]
        public string TransferStatus { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
