using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class ShowMasters
    {

        [Required]
        public int MasterId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? UserPhoto { get; set; }
    }
}
