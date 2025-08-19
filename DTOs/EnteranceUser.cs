using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class EnteranceUser
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [EmailAddress(ErrorMessage = "Email должен быть введен в формате example@domain.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string PasswordHash { get; set; }
    }
}
