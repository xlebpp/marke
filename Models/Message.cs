using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Message
    {
        [Required] 
        public int Id { get; set; }
        [Required] 
        public int ChatId { get; set; }
        [Required] 
        public Chat Chat { get; set; }
        [Required] 
        public int SenderId { get; set; }
        [Required] 
        public User Sender { get; set; }
        [Required] 
        public string Text { get; set; }
        [Required] 
        public DateTime SendAt {  get; set; } = DateTime.UtcNow;
        [Required] 
        public bool IsRead { get; set; } = false;
    }
}
