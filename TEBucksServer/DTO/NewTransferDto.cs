namespace TEBucksServer.DTO
{
    public class NewTransferDto
    {
        public string userFrom { get; set; }
        public string userTo { get; set; }
        public decimal amount { get; set; }
        public string transferType { get; set; }
    }
}
