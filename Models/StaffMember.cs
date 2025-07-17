using System.ComponentModel.DataAnnotations;

namespace BarberSalonPrototype.Models
{
    public class StaffMember
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        [StringLength(100, ErrorMessage = "Role cannot exceed 100 characters")]
        [Display(Name = "Role")]
        public string Role { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Image URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Specializations")]
        public List<StaffSpecialization> Specializations { get; set; } = new List<StaffSpecialization>();

        [Display(Name = "Years of Experience")]
        [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50")]
        public int YearsOfExperience { get; set; } = 0;

        [Display(Name = "Instagram URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? InstagramUrl { get; set; }

        [Display(Name = "Facebook URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? FacebookUrl { get; set; }

        [Display(Name = "TikTok URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? TikTokUrl { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; } = 0;

        [Display(Name = "Gallery")]
        public List<GalleryImage> Gallery { get; set; } = new List<GalleryImage>();

        // Computed properties
        public string DisplayName => $"{FullName} - {Role}";
        public bool HasSocialMedia => !string.IsNullOrEmpty(InstagramUrl) || !string.IsNullOrEmpty(FacebookUrl) || !string.IsNullOrEmpty(TikTokUrl);
        public string ExperienceText => YearsOfExperience == 1 ? "1 year" : $"{YearsOfExperience} years";
    }
} 