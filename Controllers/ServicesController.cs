using Microsoft.AspNetCore.Mvc;

namespace BarberSalonPrototype.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            var services = new List<object>
            {
                new { Name = "Fade Cut", Description = "Classic fade haircut with clean lines and modern styling", Price = "R120" },
                new { Name = "Beard Trim", Description = "Professional beard trimming and shaping service", Price = "R80" },
                new { Name = "Haircut & Style", Description = "Complete haircut with professional styling", Price = "R150" },
                new { Name = "Kids Haircut", Description = "Specialized haircuts for children under 12", Price = "R90" },
                new { Name = "Hair Color", Description = "Professional hair coloring and highlights", Price = "R200" },
                new { Name = "Hair Treatment", Description = "Deep conditioning and hair treatment", Price = "R180" },
                new { Name = "Manicure", Description = "Classic manicure with nail care", Price = "R120" },
                new { Name = "Pedicure", Description = "Relaxing pedicure with foot care", Price = "R150" },
                new { Name = "Facial", Description = "Rejuvenating facial treatment", Price = "R250" }
            };

            return View(services);
        }
    }
} 