using marketplaceE.Models;

namespace marketplaceE.DTOs
{
    public class ShowUserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? UserPhoto { get; set; }

        public string? About { get; set; }
        
        public List<Request>? Requests { get; set; } = new();
    }
}
