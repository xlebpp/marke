using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Category
    {
        [Required] 
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }    
        public string Description { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<Request> Requests { get; set; } = new();
    }
}
