using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TEBucksServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        [Required]
        public int UserFrom { get; set; }
        [Required]
        public int UserTo { get; set; }
        [Required]
        public string TransferType { get; set; }

        //TODO maybe we can do this as a derived property based on the  transfer type
        public string TransferStatus { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
