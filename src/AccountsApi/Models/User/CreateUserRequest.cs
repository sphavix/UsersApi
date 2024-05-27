using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AccountsApi.Entities;

namespace AccountsApi.Models.User
{
    public class CreateUserRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    }
}