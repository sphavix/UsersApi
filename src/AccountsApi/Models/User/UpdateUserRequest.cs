using System.ComponentModel.DataAnnotations;
using AccountsApi.Entities;

namespace AccountsApi.Models.User
{
    public class UpdateUserRequest
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        private string _password;
        [MinLength(6)]
        public string Password
        {
            get => _password;
            set => _password = BCrypt.Net.BCrypt.HashPassword(value);
        
        }

        private string _confirmPassword;
        [Compare("Password")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = BCrypt.Net.BCrypt.HashPassword(value);
        }

        [Required]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    
    }
}