using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Request
    {
        [Required]
        public int Id { get; set; }
        [Required] 
        public string Title { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required] 
        public int CustomerId {  get; set; }
        [Required] 
        public User Customer { get; set; }

        [Required] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Category> Categories { get; set; } = new();


    }
}
