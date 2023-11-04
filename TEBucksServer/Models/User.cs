using System.Text.Json.Serialization;

namespace TEBucksServer.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string Salt { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// Model to return upon successful login
    /// </summary>
    public class ReturnUser
    {
        public User User { get; set; }
        //public string Role { get; set; }
        public string Token { get; set; }
    }

    public class IdName
    {
        [JsonPropertyName("id")]
        public int UserId { get; set; }
        public string Username { get; set; }
    }

    /// <summary>
    /// Model to accept login parameters
    /// </summary>
    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
