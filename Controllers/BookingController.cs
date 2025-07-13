using Microsoft.AspNetCore.Mvc;
using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;

namespace BarberSalonPrototype.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IServiceService _serviceService;
        private readonly IStaffService _staffService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(
            IBookingService bookingService,
            IServiceService serviceService,
            IStaffService staffService,
            ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _serviceService = serviceService;
            _staffService = staffService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Loading booking page");
                
                var services = await _serviceService.GetAllServicesAsync();
                var staffMembers = await _staffService.GetActiveStaffAsync();
                
                var viewModel = new BookingViewModel
                {
                    Services = services.ToList(),
                    StaffMembers = staffMembers.ToList(),
                    Booking = new Booking()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading booking page");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid booking data submitted");
                    
                    var services = await _serviceService.GetAllServicesAsync();
                    var staffMembers = await _staffService.GetActiveStaffAsync();
                    
                    var viewModel = new BookingViewModel
                    {
                        Services = services.ToList(),
                        StaffMembers = staffMembers.ToList(),
                        Booking = booking
                    };
                    
                    return View("Index", viewModel);
                }

                _logger.LogInformation("Creating booking for: {Name} ({Email})", 
                    booking.FullName, booking.Email);

                // Validate appointment date is in the future
                if (booking.AppointmentDate.Date <= DateTime.Today)
                {
                    ModelState.AddModelError("AppointmentDate", "Appointment date must be in the future");
                    return View("Index", new BookingViewModel
                    {
                        Services = (await _serviceService.GetAllServicesAsync()).ToList(),
                        StaffMembers = (await _staffService.GetActiveStaffAsync()).ToList(),
                        Booking = booking
                    });
                }

                // Check if time slot is available
                var isAvailable = await _bookingService.IsTimeSlotAvailableAsync(
                    booking.AppointmentDateTime, booking.ServiceId, booking.StaffMemberId);
                
                if (!isAvailable)
                {
                    ModelState.AddModelError("AppointmentTime", "This time slot is not available. Please select a different time.");
                    return View("Index", new BookingViewModel
                    {
                        Services = (await _serviceService.GetAllServicesAsync()).ToList(),
                        StaffMembers = (await _staffService.GetActiveStaffAsync()).ToList(),
                        Booking = booking
                    });
                }

                var createdBooking = await _bookingService.CreateBookingAsync(booking);
                
                _logger.LogInformation("Booking created successfully with ID: {BookingId}", createdBooking.Id);
                
                TempData["SuccessMessage"] = $"Booking confirmed! Your appointment is scheduled for {booking.AppointmentDate:dddd, MMMM dd, yyyy} at {booking.AppointmentTime}.";
                
                return RedirectToAction(nameof(Confirmation), new { id = createdBooking.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                ModelState.AddModelError("", "An error occurred while creating your booking. Please try again.");
                
                var services = await _serviceService.GetAllServicesAsync();
                var staffMembers = await _staffService.GetActiveStaffAsync();
                
                var viewModel = new BookingViewModel
                {
                    Services = services.ToList(),
                    StaffMembers = staffMembers.ToList(),
                    Booking = booking
                };
                
                return View("Index", viewModel);
            }
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            try
            {
                _logger.LogInformation("Loading booking confirmation for ID: {Id}", id);
                
                var booking = await _bookingService.GetBookingByIdAsync(id);
                
                if (booking == null)
                {
                    _logger.LogWarning("Booking not found with ID: {Id}", id);
                    return NotFound();
                }

                return View(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading booking confirmation for ID: {Id}", id);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTimeSlots(DateTime date, int serviceId, int staffId)
        {
            try
            {
                _logger.LogInformation("Getting available time slots for date: {Date}, service: {ServiceId}, staff: {StaffId}", 
                    date, serviceId, staffId);
                
                var timeSlots = await _bookingService.GetAvailableTimeSlotsAsync(date, serviceId, staffId);
                
                return Json(new { success = true, timeSlots = timeSlots });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available time slots");
                return Json(new { success = false, message = "An error occurred while loading time slots" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return View(new List<Booking>());
                }

                _logger.LogInformation("Loading bookings for email: {Email}", email);
                
                // In a real application, you would filter by email
                // For now, we'll return all upcoming bookings
                var bookings = await _bookingService.GetUpcomingBookingsAsync();
                
                return View(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading bookings for email: {Email}", email);
                return RedirectToAction("Error", "Home");
            }
        }
    }

    public class BookingViewModel
    {
        public List<Service> Services { get; set; } = new List<Service>();
        public List<StaffMember> StaffMembers { get; set; } = new List<StaffMember>();
        public Booking Booking { get; set; } = new Booking();
    }
} 