using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Product
    {
        [Required] 
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required] 
        public bool IsService   { get; set; }
        [Required] 
        public decimal Price { get; set; }
        [Required] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required] 
        public int MasterId { get; set; }
        [Required] 
        public User Master { get; set; }

        public List<Category> Categories { get; set; } = new();
        public List<ProductImages> Images { get; set; } = new();
    }
}
