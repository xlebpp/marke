using marketplaceE.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class TokenUSerDto
    {
        [Required]
        public RolesOfUsers Role { get; set; }

        [Required] 
        public string Email { get; set; }
        [Required] 
        public string UserName { get; set; }
    }
}
