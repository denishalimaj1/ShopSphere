using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShopSphere.Models
{
    public class GetUserDetails
    {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
