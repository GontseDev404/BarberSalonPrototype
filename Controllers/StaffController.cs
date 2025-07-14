using Microsoft.AspNetCore.Mvc;
using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;

namespace BarberSalonPrototype.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly ILogger<StaffController> _logger;

        public StaffController(IStaffService staffService, ILogger<StaffController> logger)
        {
            _staffService = staffService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Loading staff index page");
                var staffMembers = await _staffService.GetActiveStaffAsync();
                return View(staffMembers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading staff index page");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                _logger.LogInformation("Loading staff details for ID: {Id}", id);
                
                var staffMember = await _staffService.GetStaffByIdAsync(id);
                
                if (staffMember == null)
                {
                    _logger.LogWarning("Staff member not found with ID: {Id}", id);
                    return NotFound();
                }

                if (!staffMember.IsActive)
                {
                    _logger.LogWarning("Inactive staff member accessed with ID: {Id}", id);
                    return NotFound();
                }

                return View(staffMember);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading staff details for ID: {Id}", id);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddGalleryImage(GalleryImage image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid gallery image data submitted");
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Adding gallery image for staff member ID: {StaffId}", image.StaffMemberId);
                
                var addedImage = await _staffService.AddGalleryImageAsync(image);
                
                return Json(new { success = true, image = addedImage });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid request for adding gallery image");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding gallery image");
                return StatusCode(500, "An error occurred while adding the image");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveGalleryImage(int imageId)
        {
            try
            {
                _logger.LogInformation("Removing gallery image with ID: {ImageId}", imageId);
                
                var success = await _staffService.RemoveGalleryImageAsync(imageId);
                
                if (!success)
                {
                    _logger.LogWarning("Gallery image not found with ID: {ImageId}", imageId);
                    return NotFound();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing gallery image with ID: {ImageId}", imageId);
                return StatusCode(500, "An error occurred while removing the image");
            }
        }

        public async Task<IActionResult> Gallery(int staffId)
        {
            try
            {
                _logger.LogInformation("Loading gallery for staff member ID: {StaffId}", staffId);
                
                var staffMember = await _staffService.GetStaffByIdAsync(staffId);
                if (staffMember == null)
                {
                    return NotFound();
                }

                var gallery = await _staffService.GetStaffGalleryAsync(staffId);
                
                var viewModel = new StaffGalleryViewModel
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
    }

} 