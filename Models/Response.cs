using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Response
    {
        [Required] 
        public int Id { get; set; }
        [Required] 
        public int SenderId { get; set; }
        [Required] 
        public User Sender { get; set; }

        public string? Message { get; set; }
        public int? ProductId {  get; set; }
        public Product? Product { get; set; }
        public int? RequestId { get; set; }
        public Request? Request { get; set; }

        [Required] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
