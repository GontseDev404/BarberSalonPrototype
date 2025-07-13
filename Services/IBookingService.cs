using BarberSalonPrototype.Models;

namespace BarberSalonPrototype.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsByDateAsync(DateTime date);
        Task<IEnumerable<Booking>> GetUpcomingBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByStatusAsync(BookingStatus status);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
        Task<bool> ConfirmBookingAsync(int id);
        Task<bool> CancelBookingAsync(int id);
        Task<IEnumerable<string>> GetAvailableTimeSlotsAsync(DateTime date, int serviceId, int staffId);
        Task<bool> IsTimeSlotAvailableAsync(DateTime dateTime, int serviceId, int staffId);
    }
} 