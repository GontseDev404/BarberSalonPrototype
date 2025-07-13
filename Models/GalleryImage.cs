using System.ComponentModel.DataAnnotations;

namespace BarberSalonPrototype.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Staff member ID is required")]
        [Display(Name = "Staff Member")]
        public int StaffMemberId { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Caption is required")]
        [StringLength(200, ErrorMessage = "Caption cannot exceed 200 characters")]
        [Display(Name = "Caption")]
        public string Caption { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; } = 0;

        [Display(Name = "Is Featured")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Upload Date")]
        public DateTime UploadDate { get; set; } = DateTime.Now;

        // Navigation property
        public StaffMember? StaffMember { get; set; }

        // Computed properties
        public string DisplayCaption => string.IsNullOrEmpty(Description) ? Caption : $"{Caption} - {Description}";
    }
} 