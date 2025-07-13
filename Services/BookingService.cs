using BarberSalonPrototype.Models;

namespace BarberSalonPrototype.Services
{
    public class BookingService : IBookingService
    {
        private readonly List<Booking> _bookings;
        private readonly ILogger<BookingService> _logger;

        public BookingService(ILogger<BookingService> logger)
        {
            _logger = logger;
            _bookings = InitializeBookingData();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all bookings");
                return await Task.FromResult(_bookings.OrderByDescending(b => b.CreatedDate));
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
                return await Task.FromResult(_bookings.FirstOrDefault(b => b.Id == id));
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
                return await Task.FromResult(_bookings.Where(b => b.AppointmentDate.Date == date.Date));
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
                return await Task.FromResult(_bookings.Where(b => b.IsUpcoming && b.Status != BookingStatus.Cancelled)
                    .OrderBy(b => b.AppointmentDateTime));
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
                return await Task.FromResult(_bookings.Where(b => b.Status == status)
                    .OrderByDescending(b => b.CreatedDate));
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
                
                booking.Id = _bookings.Max(b => b.Id) + 1;
                booking.CreatedDate = DateTime.Now;
                booking.Status = BookingStatus.Pending;
                
                _bookings.Add(booking);
                
                return await Task.FromResult(booking);
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
                
                var existingBooking = _bookings.FirstOrDefault(b => b.Id == booking.Id);
                if (existingBooking == null)
                {
                    throw new ArgumentException($"Booking with ID {booking.Id} not found");
                }

                var index = _bookings.IndexOf(existingBooking);
                _bookings[index] = booking;
                
                return await Task.FromResult(booking);
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
                
                var booking = _bookings.FirstOrDefault(b => b.Id == id);
                if (booking == null)
                {
                    return false;
                }

                return await Task.FromResult(_bookings.Remove(booking));
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
                
                var booking = _bookings.FirstOrDefault(b => b.Id == id);
                if (booking == null)
                {
                    return false;
                }

                booking.Status = BookingStatus.Confirmed;
                return await Task.FromResult(true);
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
                
                var booking = _bookings.FirstOrDefault(b => b.Id == id);
                if (booking == null)
                {
                    return false;
                }

                booking.Status = BookingStatus.Cancelled;
                return await Task.FromResult(true);
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
                var existingBookings = _bookings.Where(b => 
                    b.AppointmentDate.Date == date.Date && 
                    b.StaffMemberId == staffId && 
                    b.Status != BookingStatus.Cancelled)
                    .Select(b => b.AppointmentTime)
                    .ToList();

                // Return available time slots
                var availableSlots = businessHours.Except(existingBookings).ToList();
                
                return await Task.FromResult(availableSlots);
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
                
                var conflictingBooking = _bookings.FirstOrDefault(b => 
                    b.AppointmentDate.Date == date &&
                    b.AppointmentTime == timeString &&
                    b.StaffMemberId == staffId &&
                    b.Status != BookingStatus.Cancelled);
                
                return await Task.FromResult(conflictingBooking == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking time slot availability");
                throw;
            }
        }

        private List<Booking> InitializeBookingData()
        {
            return new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "+27123456789",
                    ServiceId = 1,
                    StaffMemberId = 1,
                    AppointmentDate = DateTime.Today.AddDays(2),
                    AppointmentTime = "10:00",
                    SpecialRequests = "Please make it extra sharp",
                    Status = BookingStatus.Confirmed,
                    CreatedDate = DateTime.Now.AddDays(-1)
                },
                new Booking
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "+27987654321",
                    ServiceId = 3,
                    StaffMemberId = 2,
                    AppointmentDate = DateTime.Today.AddDays(1),
                    AppointmentTime = "14:30",
                    Status = BookingStatus.Pending,
                    CreatedDate = DateTime.Now.AddHours(-2)
                }
            };
        }
    }
} 