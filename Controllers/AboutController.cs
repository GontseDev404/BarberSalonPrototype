using Microsoft.AspNetCore.Mvc;

namespace BarberSalonPrototype.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 