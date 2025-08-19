using System.ComponentModel.DataAnnotations;

namespace marketplaceE.Models
{
    public class Chat
    {
        [Required] 
        public int Id { get; set; }
        [Required] 
        public string ChatName { get; set; }
        [Required] 
        public int User1Id { get; set; }
        [Required] 
        public User User1 {  get; set; }
        [Required] 
        public int User2Id { get; set;}
        [Required] 
        public User User2 { get; set; }
        [Required] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Message> Messages { get; set; }


    }
}
