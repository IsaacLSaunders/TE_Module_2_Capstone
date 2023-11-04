using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TEBucksServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public User UserFrom { get; set; }
        public User UserTo { get; set; }
        public string TransferType { get; set; }
        public string TransferStatus { get; set; }
        public decimal Amount { get; set; }
    }
}
