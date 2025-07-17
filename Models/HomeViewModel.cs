using System.ComponentModel.DataAnnotations;

namespace BarberSalonPrototype.Models
{
    public class HomeViewModel
    {
        [Display(Name = "Popular Services")]
        public List<Service> PopularServices { get; set; } = new List<Service>();
        
        [Display(Name = "Featured Staff")]
        public List<StaffMember> FeaturedStaff { get; set; } = new List<StaffMember>();
        
        [Display(Name = "Latest Gallery Images")]
        public List<GalleryImage> LatestGalleryImages { get; set; } = new List<GalleryImage>();
        
        // Computed properties for the view
        public bool HasPopularServices => PopularServices.Any();
        public bool HasFeaturedStaff => FeaturedStaff.Any();
        public bool HasGalleryImages => LatestGalleryImages.Any();
        
        public string WelcomeMessage { get; set; } = "Welcome to Groom & Glow";
        public string SubheadingMessage { get; set; } = "Premium Grooming Experience in Sandton";
    }
}