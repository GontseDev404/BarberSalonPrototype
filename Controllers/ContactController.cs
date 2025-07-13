using Microsoft.AspNetCore.Mvc;
using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;

namespace BarberSalonPrototype.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("Loading contact page");
                return View(new ContactMessage());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading contact page");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactMessage contactMessage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid contact form submission");
                    return View("Index", contactMessage);
                }

                _logger.LogInformation("Contact message received from: {Name} ({Email}) - Subject: {Subject}", 
                    contactMessage.Name, contactMessage.Email, contactMessage.Subject);

                // Here you would typically:
                // 1. Save to database
                // 2. Send email notification
                // 3. Send confirmation email to user
                
                // For now, we'll just log it and show success message
                
                TempData["SuccessMessage"] = "Thank you for your message! We'll get back to you within 24 hours.";
                
                _logger.LogInformation("Contact message processed successfully for: {Email}", contactMessage.Email);
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing contact message from: {Email}", contactMessage.Email);
                ModelState.AddModelError("", "An error occurred while sending your message. Please try again later.");
                return View("Index", contactMessage);
            }
        }

        [HttpGet]
        public IActionResult GetContactInfo()
        {
            try
            {
                var contactInfo = new
                {
                    Address = "123 Main Street, Cape Town, South Africa",
                    Phone = "+27 21 123 4567",
                    Email = "info@barbersalon.co.za",
                    Hours = new
                    {
                        Monday = "9:00 AM - 6:00 PM",
                        Tuesday = "9:00 AM - 6:00 PM",
                        Wednesday = "9:00 AM - 6:00 PM",
                        Thursday = "9:00 AM - 6:00 PM",
                        Friday = "9:00 AM - 6:00 PM",
                        Saturday = "9:00 AM - 4:00 PM",
                        Sunday = "Closed"
                    }
                };

                return Json(new { success = true, data = contactInfo });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving contact information");
                return Json(new { success = false, message = "Error retrieving contact information" });
            }
        }
    }
} 