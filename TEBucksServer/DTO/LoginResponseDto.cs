using TEBucksServer.Models;

namespace TEBucksServer.DTO
{
    public class LoginResponseDto
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
