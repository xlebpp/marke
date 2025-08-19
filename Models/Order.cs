using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required] 
        public User Customer { get; set; }
        [Required] 
        public int MasterId { get; set; }
        [Required] 
        public User Master { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? RequestId { get; set; }
        public Request? Request { get; set; }
        [Required] 
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public Review? Review { get; set; }

        [Required] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public enum OrderStatus
    {
        Created = 0,
        InProgress = 1,
        Completed = 2,
        Canceled = 3
    }
}
