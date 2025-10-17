using marketplaceE.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class ShowMasterProfile
    {
        public int Id { get; set; }
            
        public string Name { get; set; }
        public string? UserPhoto { get; set; }

        public string? About { get; set; }
        public double? Rating { get; set; }
        public List<Product>? Products { get; set; } = new();
    }
}

