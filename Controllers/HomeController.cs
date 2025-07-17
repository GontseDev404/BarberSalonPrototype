using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;

namespace BarberSalonPrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceService _serviceService;
        private readonly IStaffService _staffService;

        public HomeController(
            ILogger<HomeController> logger,
            IServiceService serviceService,
            IStaffService staffService)
        {
            _logger = logger;
            _serviceService = serviceService;
            _staffService = staffService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Loading home page");
                
                var popularServices = await _serviceService.GetPopularServicesAsync();
                var activeStaff = await _staffService.GetActiveStaffAsync();
                
                var viewModel = new HomeViewModel
                {
                    PopularServices = popularServices.Take(3).ToList(),
                    FeaturedStaff = activeStaff.Take(3).ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page");
                return RedirectToAction(nameof(Error));
            }
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Loading privacy page");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogWarning("Error page accessed. Request ID: {RequestId}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }

        public new IActionResult NotFound()
        {
            if (HttpContext.Request.Path != "/Home/NotFound")
                _logger.LogWarning("404 page accessed for URL: {Url}", HttpContext.Request.Path);
            return View("~/Views/Shared/NotFound.cshtml");
        }

        [HttpPost]
        public IActionResult Contact(ContactMessage contactMessage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid contact form submission");
                    return View("Contact", contactMessage);
                }

                _logger.LogInformation("Contact form submitted by: {Name} ({Email})", 
                    contactMessage.Name, contactMessage.Email);

                // Here you would typically save to database or send email
                // For now, we'll just log it
                
                TempData["SuccessMessage"] = "Thank you for your message! We'll get back to you soon.";
                return RedirectToAction(nameof(Contact));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing contact form submission");
                ModelState.AddModelError("", "An error occurred while sending your message. Please try again.");
                return View("Contact", contactMessage);
            }
        }
    }
} 