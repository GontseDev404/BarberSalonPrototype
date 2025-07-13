namespace BarberSalonPrototype.Models
{
    public class StaffMember
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Specializations { get; set; } = new List<string>();
        public string InstagramUrl { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string TikTokUrl { get; set; } = string.Empty;
        public List<GalleryImage> Gallery { get; set; } = new List<GalleryImage>();
    }
} 