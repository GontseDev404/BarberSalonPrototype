using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;
using BarberSalonPrototype.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberSalonPrototype.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookingService _bookingService;
        private readonly IServiceService _serviceService;
        private readonly IStaffService _staffService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IBookingService bookingService,
            IServiceService serviceService,
            IStaffService staffService,
            ILogger<AdminController> logger)
        {
            _context = context;
            _userManager = userManager;
            _bookingService = bookingService;
            _serviceService = serviceService;
            _staffService = staffService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Loading admin dashboard");

                var viewModel = new AdminDashboardViewModel
                {
                    TotalBookings = await _context.Bookings.CountAsync(),
                    TodayBookings = await _context.Bookings.CountAsync(b => b.AppointmentDate.Date == DateTime.Today),
                    PendingBookings = await _context.Bookings.CountAsync(b => b.Status == BookingStatus.Pending),
                    TotalServices = await _context.Services.CountAsync(),
                    TotalStaff = await _context.StaffMembers.CountAsync(s => s.IsActive),
                    TotalUsers = await _userManager.Users.CountAsync(),
                    
                    RecentBookings = await _context.Bookings
                        .Include(b => b.Service)
                        .Include(b => b.StaffMember)
                        .OrderByDescending(b => b.CreatedDate)
                        .Take(10)
                        .ToListAsync(),
                        
                    PopularServices = await _context.Services
                        .Where(s => s.IsPopular)
                        .OrderBy(s => s.SortOrder)
                        .Take(5)
                        .ToListAsync(),
                        
                    RecentUsers = await _userManager.Users
                        .OrderByDescending(u => u.DateJoined)
                        .Take(5)
                        .ToListAsync()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading admin dashboard");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Bookings()
        {
            try
            {
                var bookings = await _context.Bookings
                    .Include(b => b.Service)
                    .Include(b => b.StaffMember)
                    .OrderByDescending(b => b.CreatedDate)
                    .ToListAsync();

                return View(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading admin bookings");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBookingStatus(int id, BookingStatus status)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return NotFound();
                }

                booking.Status = status;
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Booking status updated to {status}";
                return RedirectToAction(nameof(Bookings));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking status for ID: {Id}", id);
                TempData["ErrorMessage"] = "Error updating booking status";
                return RedirectToAction(nameof(Bookings));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            try
            {
                var users = await _userManager.Users
                    .OrderByDescending(u => u.DateJoined)
                    .ToListAsync();

                var userViewModels = new List<UserViewModel>();
                
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    userViewModels.Add(new UserViewModel
                    {
                        User = user,
                        Roles = roles.ToList()
                    });
                }

                return View(userViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading admin users");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Services()
        {
            try
            {
                var services = await _context.Services
                    .OrderBy(s => s.Category)
                    .ThenBy(s => s.SortOrder)
                    .ToListAsync();

                return View(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading admin services");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Staff()
        {
            try
            {
                var staff = await _context.StaffMembers
                    .Include(s => s.Gallery)
                    .OrderBy(s => s.SortOrder)
                    .ToListAsync();

                return View(staff);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading admin staff");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Reports()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading admin reports");
                return RedirectToAction("Error", "Home");
            }
        }
    }

    public class AdminDashboardViewModel
    {
        public int TotalBookings { get; set; }
        public int TodayBookings { get; set; }
        public int PendingBookings { get; set; }
        public int TotalServices { get; set; }
        public int TotalStaff { get; set; }
        public int TotalUsers { get; set; }
        public List<Booking> RecentBookings { get; set; } = new List<Booking>();
        public List<Service> PopularServices { get; set; } = new List<Service>();
        public List<ApplicationUser> RecentUsers { get; set; } = new List<ApplicationUser>();
    }

    public class UserViewModel
    {
        public ApplicationUser User { get; set; } = new ApplicationUser();
        public List<string> Roles { get; set; } = new List<string>();
    }
}