using System.Collections.Generic;

namespace BarberSalonPrototype.Models
{
    public class GalleryViewModel
    {
        public List<GalleryImage> AllImages { get; set; } = new List<GalleryImage>();
        public List<GalleryImage> FeaturedImages { get; set; } = new List<GalleryImage>();
        public List<StaffMember> StaffMembers { get; set; } = new List<StaffMember>();
    }

    public class StaffGalleryViewModel
    {
        public StaffMember StaffMember { get; set; } = new StaffMember();
        public List<GalleryImage> Gallery { get; set; } = new List<GalleryImage>();
    }
}