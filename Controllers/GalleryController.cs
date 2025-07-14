using Microsoft.AspNetCore.Mvc;
using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;

namespace BarberSalonPrototype.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IStaffService staffService, ILogger<GalleryController> logger)
        {
            _staffService = staffService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("Loading gallery page");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading gallery page: {Message}", ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> Staff(int staffId)
        {
            try
            {
                _logger.LogInformation("Loading gallery for staff member ID: {StaffId}", staffId);
                
                var staffMember = await _staffService.GetStaffByIdAsync(staffId);
                if (staffMember == null)
                {
                    _logger.LogWarning("Staff member not found with ID: {StaffId}", staffId);
                    return NotFound();
                }

                var gallery = await _staffService.GetStaffGalleryAsync(staffId);
                
                var viewModel = new BarberSalonPrototype.Models.StaffGalleryViewModel
                {
                    StaffMember = staffMember,
                    Gallery = gallery.ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading gallery for staff member ID: {StaffId}", staffId);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryImages(int? staffId = null)
        {
            try
            {
                _logger.LogInformation("Getting gallery images for staff ID: {StaffId}", staffId);
                
                if (staffId.HasValue)
                {
                    var gallery = await _staffService.GetStaffGalleryAsync(staffId.Value);
                    return Json(new { success = true, images = gallery });
                }
                else
                {
                    var allStaff = await _staffService.GetAllStaffAsync();
                    var allImages = new List<GalleryImage>();
                    
                    foreach (var staff in allStaff)
                    {
                        allImages.AddRange(staff.Gallery);
                    }
                    
                    return Json(new { success = true, images = allImages.OrderByDescending(i => i.UploadDate) });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting gallery images");
                return Json(new { success = false, message = "Error retrieving gallery images" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFeaturedImages()
        {
            try
            {
                _logger.LogInformation("Getting featured gallery images");
                
                var allStaff = await _staffService.GetAllStaffAsync();
                var featuredImages = new List<GalleryImage>();
                
                foreach (var staff in allStaff)
                {
                    featuredImages.AddRange(staff.Gallery.Where(i => i.IsFeatured));
                }
                
                return Json(new { success = true, images = featuredImages.OrderBy(i => i.SortOrder) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting featured gallery images");
                return Json(new { success = false, message = "Error retrieving featured images" });
            }
        }
    }
} 