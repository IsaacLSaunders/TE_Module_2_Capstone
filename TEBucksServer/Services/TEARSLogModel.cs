using System;

namespace TEBucksServer.Services
{
    public class TEARSLogModel
    {
        public string description { get; set; }
        public string username_from { get; set; }
        public string username_to { get; set; }
        public decimal amount { get; set; }
        public int log_id { get; set; }
        public DateTime createdDate { get; set; }
    }

}
