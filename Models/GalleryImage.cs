namespace BarberSalonPrototype.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public int StaffMemberId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        
        // Navigation property
        public StaffMember? StaffMember { get; set; }
    }
} 