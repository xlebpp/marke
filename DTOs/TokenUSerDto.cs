using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class TokenUSerDto
    {
        [Required]
        public string Role { get; set; }

        [Required] 
        public string Email { get; set; }
        [Required] 
        public string UserName { get; set; }
    }
}
