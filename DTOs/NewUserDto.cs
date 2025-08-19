using System.ComponentModel.DataAnnotations;
using marketplaceE.Models;

namespace marketplaceE.DTOs
{
    public class NewUserDto
    {
      
        [Required (ErrorMessage = "Поле должно быть заполнено")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [EmailAddress(ErrorMessage = "Email должен быть введен в формате example@domain.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")] 
        public string PasswordHash { get; set; }
        [Required (ErrorMessage = "Поле должно быть заполнено")] 
        public RolesOfUsers Role { get; set; }
        [Required (ErrorMessage = "Поле должно быть заполнено")] 
        public DateOnly DateOfBirth { get; set; }
    }
}
