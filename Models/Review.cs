using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Review
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public User Author { get; set; }

        [Required] 
        public int ReceiverId { get; set; }
        [Required] 
        public User Receiver { get; set; }
        [Required] 
        public int OrderId { get; set; }
        [Required] 
        public Order Order { get; set; }

        [Required] 
        public int Rating { get; set; }
        [Required] 
        public string Text { get; set; }
        [Required] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
