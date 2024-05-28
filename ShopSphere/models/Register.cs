using System.Text.Json.Serialization;

namespace ShopSphere.Models
{
    
    public class UserRegistrationModel
    {
        // Exclude id from JSON serialization
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        // Add other properties as needed for user registration
        
        // You can also include data annotations for validation if necessary
        // For example:
        // [Required(ErrorMessage = "Username is required")]
        // [EmailAddress(ErrorMessage = "Invalid email address")]
        // [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        // etc.
    }
}
