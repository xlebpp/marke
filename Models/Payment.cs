using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Payment
    {
        [Required] 
        public int Id { get; set; }

        [Required] 
        public int OrderId { get; set; }

        [Required] 
        public Order Order { get; set; }

        [Required] 
        public decimal Amount { get; set; }

        [Required] 
        public string PaymentMethod { get; set; }
        [Required] 
        public DateTime PaidAt { get; set; }
    }
}
