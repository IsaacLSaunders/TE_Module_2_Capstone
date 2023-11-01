using System.ComponentModel.DataAnnotations;

namespace TEBucksServer.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public int LoginId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
