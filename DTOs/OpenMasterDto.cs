using marketplaceE.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class OpenMasterDto
    {
        [Required]
        public int MasterId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? UserPhoto { get; set; }
        public List<Product?> Products { get; set; }
        public double Rating { get; set; }
    }
}
