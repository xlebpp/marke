using System.ComponentModel.DataAnnotations;

namespace marketplaceE.DTOs
{
    public class ShowProducts
    {
        [Required]
        public string Name { get; set; }
        public bool IsService { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int MasterId { get; set; }
        [Required]
        public string MasterName { get; set; }
        [Required]
        public List<string> Images{ get; set; }
    }
}
