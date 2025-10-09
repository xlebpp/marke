using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class ShowProducts
    {
        [Required]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool IsService { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int MasterId { get; set; }
        [Required]
        public string MasterName { get; set; }
        public double Rating { get; set; }
        [Required]
        public List<string> Images { get; set; }
        public List<string> Categories {  get; set; }
    }
}
