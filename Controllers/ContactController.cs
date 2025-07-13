using Microsoft.AspNetCore.Mvc;

namespace BarberSalonPrototype.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 