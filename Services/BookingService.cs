using BarberSalonPrototype.Models;
using BarberSalonPrototype.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberSalonPrototype.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookingService> _logger;

        public BookingService(ApplicationDbContext context, ILogger<BookingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all bookings");
                return await _context.Bookings
                    .Include(b => b.Service)
                    .Include(b => b.StaffMember)
                    .OrderByDescending(b => b.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all bookings");
                throw;
            }
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving booking with ID: {Id}", id);
                return await _context.Bookings
                    .Include(b => b.Service)
                    .Include(b => b.StaffMember)
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving booking with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDateAsync(DateTime date)
        {
            try
            {
                _logger.LogInformation("Retrieving bookings for date: {Date}", date);
                return await _context.Bookings
                    .Include(b => b.Service)
                    .Include(b => b.StaffMember)
                    .Where(b => b.AppointmentDate.Date == date.Date)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bookings for date: {Date}", date);
                throw;
            }
        }

        public async Task<IEnumerable<Booking>> GetUpcomingBookingsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving upcoming bookings");
                var currentDateTime = DateTime.Now;
                return await _context.Bookings
                    .Include(b => b.Service)
                    .Include(b => b.StaffMember)
                    .Where(b => b.AppointmentDateTime > currentDateTime && b.Status != BookingStatus.Cancelled)
                    .OrderBy(b => b.AppointmentDateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving upcoming bookings");
                throw;
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingsByStatusAsync(BookingStatus status)
        {
            try
            {
                _logger.LogInformation("Retrieving bookings by status: {Status}", status);
                return await _context.Bookings
                    .Include(b => b.Service)
                    .Include(b => b.StaffMember)
                    .Where(b => b.Status == status)
                    .OrderByDescending(b => b.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bookings by status: {Status}", status);
                throw;
            }
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            try
            {
                _logger.LogInformation("Creating new booking for: {Name}", booking.FullName);
                
                booking.CreatedDate = DateTime.Now;
                booking.Status = BookingStatus.Pending;
                
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                
                // Reload with navigation properties
                await _context.Entry(booking)
                    .Reference(b => b.Service)
                    .LoadAsync();
                await _context.Entry(booking)
                    .Reference(b => b.StaffMember)
                    .LoadAsync();
                
                return booking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking for: {Name}", booking.FullName);
                throw;
            }
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            try
            {
                _logger.LogInformation("Updating booking with ID: {Id}", booking.Id);
                
                var existingBooking = await _context.Bookings.FindAsync(booking.Id);
                if (existingBooking == null)
                {
                    throw new ArgumentException($"Booking with ID {booking.Id} not found");
                }

                _context.Entry(existingBooking).CurrentValues.SetValues(booking);
                await _context.SaveChangesAsync();
                
                // Reload with navigation properties
                await _context.Entry(existingBooking)
                    .Reference(b => b.Service)
                    .LoadAsync();
                await _context.Entry(existingBooking)
                    .Reference(b => b.StaffMember)
                    .LoadAsync();
                
                return existingBooking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking with ID: {Id}", booking.Id);
                throw;
            }
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting booking with ID: {Id}", id);
                
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return false;
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booking with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ConfirmBookingAsync(int id)
        {
            try
            {
                _logger.LogInformation("Confirming booking with ID: {Id}", id);
                
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return false;
                }

                booking.Status = BookingStatus.Confirmed;
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming booking with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> CancelBookingAsync(int id)
        {
            try
            {
                _logger.LogInformation("Cancelling booking with ID: {Id}", id);
                
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return false;
                }

                booking.Status = BookingStatus.Cancelled;
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling booking with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetAvailableTimeSlotsAsync(DateTime date, int serviceId, int staffId)
        {
            try
            {
                _logger.LogInformation("Getting available time slots for date: {Date}, service: {ServiceId}, staff: {StaffId}", 
                    date, serviceId, staffId);
                
                // Business hours: 9 AM to 6 PM
                var businessHours = new List<string>
                {
                    "09:00", "09:30", "10:00", "10:30", "11:00", "11:30",
                    "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                    "15:00", "15:30", "16:00", "16:30", "17:00", "17:30"
                };

                // Get existing bookings for this date and staff member
                var existingBookings = await _context.Bookings
                    .Where(b => b.AppointmentDate.Date == date.Date && 
                               b.StaffMemberId == staffId && 
                               b.Status != BookingStatus.Cancelled)
                    .Select(b => b.AppointmentTime)
                    .ToListAsync();

                // Return available time slots
                var availableSlots = businessHours.Except(existingBookings).ToList();
                
                return availableSlots;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available time slots");
                throw;
            }
        }

        public async Task<bool> IsTimeSlotAvailableAsync(DateTime dateTime, int serviceId, int staffId)
        {
            try
            {
                _logger.LogInformation("Checking time slot availability for: {DateTime}, service: {ServiceId}, staff: {StaffId}", 
                    dateTime, serviceId, staffId);
                
                var timeString = dateTime.ToString("HH:mm");
                var date = dateTime.Date;
                
                var conflictingBooking = await _context.Bookings.FirstOrDefaultAsync(b => 
                    b.AppointmentDate.Date == date &&
                    b.AppointmentTime == timeString &&
                    b.StaffMemberId == staffId &&
                    b.Status != BookingStatus.Cancelled);
                
                return conflictingBooking == null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking time slot availability");
                throw;
            }
        }
    }
} 