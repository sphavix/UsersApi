using System.Text.Json.Serialization;

namespace AccountsApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }
}