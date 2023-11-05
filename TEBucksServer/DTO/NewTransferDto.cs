namespace TEBucksServer.DTO
{
    public class NewTransferDto
    {
        public int userFrom { get; set; }
        public int userTo { get; set; }
        public decimal amount { get; set; }
        public string transferType { get; set; }
    }
}
