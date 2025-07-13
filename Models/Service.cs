using System.ComponentModel.DataAnnotations;

namespace BarberSalonPrototype.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters")]
        [Display(Name = "Service Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Service description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [RegularExpression(@"^R\d+(\.\d{2})?$", ErrorMessage = "Price must be in format R###.##")]
        [Display(Name = "Price")]
        public string Price { get; set; } = string.Empty;

        [Display(Name = "Duration (minutes)")]
        [Range(15, 300, ErrorMessage = "Duration must be between 15 and 300 minutes")]
        public int DurationMinutes { get; set; } = 60;

        [Display(Name = "Category")]
        public ServiceCategory Category { get; set; } = ServiceCategory.Haircut;

        [Display(Name = "Is Popular")]
        public bool IsPopular { get; set; } = false;

        [Display(Name = "Image URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; } = 0;
    }

    public enum ServiceCategory
    {
        [Display(Name = "Haircut")]
        Haircut,
        [Display(Name = "Beard & Grooming")]
        BeardGrooming,
        [Display(Name = "Hair Treatment")]
        HairTreatment,
        [Display(Name = "Nail Care")]
        NailCare,
        [Display(Name = "Facial & Skincare")]
        FacialSkincare,
        [Display(Name = "Styling")]
        Styling
    }
} 