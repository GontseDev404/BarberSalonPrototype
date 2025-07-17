using Microsoft.AspNetCore.Mvc;
using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;

namespace BarberSalonPrototype.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServiceService serviceService, ILogger<ServicesController> logger)
        {
            _serviceService = serviceService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Loading services page");
                var allServices = await _serviceService.GetAllServicesAsync();
                var categories = await _serviceService.GetServiceCategoriesAsync();
                var viewModel = new ServicesViewModel
                {
                    Services = allServices.ToList(),
                    Categories = categories.ToList(),
                    SelectedCategory = null
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading services page");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Category(ServiceCategory category)
        {
            try
            {
                _logger.LogInformation("Loading services for category: {Category}", category);
                
                var services = await _serviceService.GetServicesByCategoryAsync(category);
                var categories = await _serviceService.GetServiceCategoriesAsync();
                
                var viewModel = new ServicesViewModel
                {
                    Services = services.ToList(),
                    Categories = categories.ToList(),
                    SelectedCategory = category
                };

                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading services for category: {Category}", category);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                _logger.LogInformation("Loading service details for ID: {Id}", id);
                
                var service = await _serviceService.GetServiceByIdAsync(id);
                
                if (service == null)
                {
                    _logger.LogWarning("Service not found with ID: {Id}", id);
                    return NotFound();
                }

                return View(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading service details for ID: {Id}", id);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Popular()
        {
            try
            {
                _logger.LogInformation("Loading popular services");
                
                var popularServices = await _serviceService.GetPopularServicesAsync();
                
                return View(popularServices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading popular services");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetServicesByCategory(ServiceCategory category)
        {
            try
            {
                _logger.LogInformation("Getting services for category: {Category}", category);
                
                var services = await _serviceService.GetServicesByCategoryAsync(category);
                
                return Json(new { success = true, services = services });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting services for category: {Category}", category);
                return Json(new { success = false, message = "An error occurred while loading services" });
            }
        }
    }
} 