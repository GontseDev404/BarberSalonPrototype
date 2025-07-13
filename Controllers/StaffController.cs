using Microsoft.AspNetCore.Mvc;
using BarberSalonPrototype.Models;

namespace BarberSalonPrototype.Controllers
{
    public class StaffController : Controller
    {
        private readonly List<StaffMember> _staffMembers;

        public StaffController()
        {
            // Initialize with sample data
            _staffMembers = new List<StaffMember>
            {
                new StaffMember
                {
                    Id = 1,
                    FullName = "Michael Rodriguez",
                    Role = "Master Barber",
                    Description = "With over 15 years of experience, Michael specializes in classic cuts, fades, and modern styling. His attention to detail and passion for the craft makes him a favorite among our clients.",
                    ImageUrl = "/images/staff/michael-rodriguez.jpg",
                    Specializations = new List<string> { "Classic Cuts", "Fades", "Beard Trimming", "Modern Styling" },
                    InstagramUrl = "https://instagram.com/michaelbarber",
                    FacebookUrl = "https://facebook.com/michaelbarber",
                    TikTokUrl = "https://tiktok.com/@michaelbarber",
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 1, StaffMemberId = 1, ImageUrl = "/images/gallery/michael-1.jpg", Caption = "Classic Pompadour" },
                        new GalleryImage { Id = 2, StaffMemberId = 1, ImageUrl = "/images/gallery/michael-2.jpg", Caption = "Modern Fade" },
                        new GalleryImage { Id = 3, StaffMemberId = 1, ImageUrl = "/images/gallery/michael-3.jpg", Caption = "Beard Styling" }
                    }
                },
                new StaffMember
                {
                    Id = 2,
                    FullName = "Sarah Johnson",
                    Role = "Hair Stylist",
                    Description = "Sarah brings creativity and innovation to every haircut. She excels in color treatments, highlights, and creating unique styles that reflect each client's personality.",
                    ImageUrl = "/images/staff/sarah-johnson.jpg",
                    Specializations = new List<string> { "Color Treatments", "Highlights", "Balayage", "Styling" },
                    InstagramUrl = "https://instagram.com/sarahstylist",
                    FacebookUrl = "https://facebook.com/sarahstylist",
                    TikTokUrl = "https://tiktok.com/@sarahstylist",
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 4, StaffMemberId = 2, ImageUrl = "/images/gallery/sarah-1.jpg", Caption = "Balayage Highlights" },
                        new GalleryImage { Id = 5, StaffMemberId = 2, ImageUrl = "/images/gallery/sarah-2.jpg", Caption = "Color Transformation" },
                        new GalleryImage { Id = 6, StaffMemberId = 2, ImageUrl = "/images/gallery/sarah-3.jpg", Caption = "Styling Session" }
                    }
                },
                new StaffMember
                {
                    Id = 3,
                    FullName = "David Chen",
                    Role = "Barber",
                    Description = "David is known for his precision cuts and ability to create clean, sharp lines. He specializes in contemporary styles and traditional barbering techniques.",
                    ImageUrl = "/images/staff/david-chen.jpg",
                    Specializations = new List<string> { "Precision Cuts", "Line-ups", "Contemporary Styles", "Traditional Techniques" },
                    InstagramUrl = "https://instagram.com/davidbarber",
                    FacebookUrl = "https://facebook.com/davidbarber",
                    TikTokUrl = "https://tiktok.com/@davidbarber",
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 7, StaffMemberId = 3, ImageUrl = "/images/gallery/david-1.jpg", Caption = "Precision Line-up" },
                        new GalleryImage { Id = 8, StaffMemberId = 3, ImageUrl = "/images/gallery/david-2.jpg", Caption = "Contemporary Cut" },
                        new GalleryImage { Id = 9, StaffMemberId = 3, ImageUrl = "/images/gallery/david-3.jpg", Caption = "Traditional Style" }
                    }
                },
                new StaffMember
                {
                    Id = 4,
                    FullName = "Emily Martinez",
                    Role = "Nail Technician",
                    Description = "Emily creates stunning nail art and provides exceptional nail care services. Her attention to detail and artistic flair make every manicure and pedicure a work of art.",
                    ImageUrl = "/images/staff/emily-martinez.jpg",
                    Specializations = new List<string> { "Nail Art", "Gel Manicures", "Acrylic Nails", "Nail Care" },
                    InstagramUrl = "https://instagram.com/emilynails",
                    FacebookUrl = "https://facebook.com/emilynails",
                    TikTokUrl = "https://tiktok.com/@emilynails",
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 10, StaffMemberId = 4, ImageUrl = "/images/gallery/emily-1.jpg", Caption = "Floral Nail Art" },
                        new GalleryImage { Id = 11, StaffMemberId = 4, ImageUrl = "/images/gallery/emily-2.jpg", Caption = "Gel Manicure" },
                        new GalleryImage { Id = 12, StaffMemberId = 4, ImageUrl = "/images/gallery/emily-3.jpg", Caption = "Acrylic Design" }
                    }
                },
                new StaffMember
                {
                    Id = 5,
                    FullName = "Alex Thompson",
                    Role = "Skin Care Specialist",
                    Description = "Alex is passionate about helping clients achieve healthy, glowing skin. She offers personalized treatments and expert advice on skincare routines.",
                    ImageUrl = "/images/staff/alex-thompson.jpg",
                    Specializations = new List<string> { "Facial Treatments", "Skin Analysis", "Anti-aging", "Acne Treatment" },
                    InstagramUrl = "https://instagram.com/alexskincare",
                    FacebookUrl = "https://facebook.com/alexskincare",
                    TikTokUrl = "https://tiktok.com/@alexskincare",
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 13, StaffMemberId = 5, ImageUrl = "/images/gallery/alex-1.jpg", Caption = "Facial Treatment" },
                        new GalleryImage { Id = 14, StaffMemberId = 5, ImageUrl = "/images/gallery/alex-2.jpg", Caption = "Skin Analysis" },
                        new GalleryImage { Id = 15, StaffMemberId = 5, ImageUrl = "/images/gallery/alex-3.jpg", Caption = "Anti-aging Treatment" }
                    }
                }
            };
        }

        public IActionResult Index()
        {
            return View(_staffMembers);
        }

        public IActionResult Details(int id)
        {
            var staffMember = _staffMembers.FirstOrDefault(s => s.Id == id);
            
            if (staffMember == null)
            {
                return NotFound();
            }

            return View(staffMember);
        }
    }
} 