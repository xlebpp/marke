using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName {  get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public RolesOfUsers Role { get; set; } = RolesOfUsers.User;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public byte[]? UserPhoto {  get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; } 

        public List<Product> Products { get; set; } = new();

        public string? About { get; set; }
    }

    public enum RolesOfUsers
    {
        Master,
        User
    }
}
