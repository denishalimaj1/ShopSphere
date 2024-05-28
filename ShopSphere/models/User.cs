using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShopSphere.Models
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The password must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        public required string PasswordHash { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
    }
}
