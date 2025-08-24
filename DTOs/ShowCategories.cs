using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class ShowCategories
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
