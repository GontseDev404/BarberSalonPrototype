using Microsoft.AspNetCore.Mvc;
using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;

namespace BarberSalonPrototype.Controllers
{
    public class AboutController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly ILogger<AboutController> _logger;

        public AboutController(IStaffService staffService, ILogger<AboutController> logger)
        {
            _staffService = staffService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Loading about page");
                
                var activeStaff = await _staffService.GetActiveStaffAsync();
                
                var viewModel = new AboutViewModel
                {
                    TotalStaff = activeStaff.Count(),
                    TotalExperience = activeStaff.Sum(s => s.YearsOfExperience),
                    StaffMembers = activeStaff.ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading about page");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult GetSalonInfo()
        {
            try
            {
                var salonInfo = new
                {
                    Name = "BarberSalon",
                    Founded = "2010",
                    Mission = "To provide exceptional grooming services in a welcoming, professional environment where every client feels valued and leaves looking and feeling their best.",
                    Vision = "To be the premier destination for men's grooming and styling, known for our expertise, quality service, and commitment to customer satisfaction.",
                    Values = new[]
                    {
                        "Excellence in Service",
                        "Professional Integrity",
                        "Customer Satisfaction",
                        "Continuous Learning",
                        "Community Involvement"
                    },
                    Achievements = new[]
                    {
                        "Best Barber Shop 2023 - Cape Town Awards",
                        "Customer Service Excellence 2022",
                        "Community Business of the Year 2021"
                    }
                };

                return Json(new { success = true, data = salonInfo });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving salon information");
                return Json(new { success = false, message = "Error retrieving salon information" });
            }
        }
    }

    public class AboutViewModel
    {
        public int TotalStaff { get; set; }
        public int TotalExperience { get; set; }
        public List<StaffMember> StaffMembers { get; set; } = new List<StaffMember>();
    }
} 