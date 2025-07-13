using Microsoft.AspNetCore.Mvc;

namespace BarberSalonPrototype.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            var galleryImages = new List<object>
            {
                new { Id = 1, Title = "Classic Fade", ImageUrl = "https://via.placeholder.com/400x300/007bff/ffffff?text=Classic+Fade", Description = "Professional fade haircut" },
                new { Id = 2, Title = "Modern Style", ImageUrl = "https://via.placeholder.com/400x300/28a745/ffffff?text=Modern+Style", Description = "Contemporary hair styling" },
                new { Id = 3, Title = "Beard Trim", ImageUrl = "https://via.placeholder.com/400x300/dc3545/ffffff?text=Beard+Trim", Description = "Precision beard grooming" },
                new { Id = 4, Title = "Hair Color", ImageUrl = "https://via.placeholder.com/400x300/ffc107/ffffff?text=Hair+Color", Description = "Professional hair coloring" },
                new { Id = 5, Title = "Kids Cut", ImageUrl = "https://via.placeholder.com/400x300/17a2b8/ffffff?text=Kids+Cut", Description = "Specialized children's haircuts" },
                new { Id = 6, Title = "Styling", ImageUrl = "https://via.placeholder.com/400x300/6f42c1/ffffff?text=Styling", Description = "Creative hair styling" },
                new { Id = 7, Title = "Nail Art", ImageUrl = "https://via.placeholder.com/400x300/fd7e14/ffffff?text=Nail+Art", Description = "Beautiful nail designs" },
                new { Id = 8, Title = "Facial Treatment", ImageUrl = "https://via.placeholder.com/400x300/20c997/ffffff?text=Facial", Description = "Rejuvenating facial care" },
                new { Id = 9, Title = "Salon Interior", ImageUrl = "https://via.placeholder.com/400x300/6c757d/ffffff?text=Salon", Description = "Our modern salon space" }
            };

            return View(galleryImages);
        }
    }
} 