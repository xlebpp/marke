using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class ProductImages
    {
        [Required] 
        public int Id { get; set; }
        [Required] 
        public byte[] Url { get; set; }

        [Required] 
        public int ProductId { get; set; }
        [Required] 
        public Product Product { get; set; }

    }
}
