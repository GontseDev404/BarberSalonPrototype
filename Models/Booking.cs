using System.ComponentModel.DataAnnotations;

namespace BarberSalonPrototype.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a service")]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Please select a staff member")]
        [Display(Name = "Preferred Staff Member")]
        public int StaffMemberId { get; set; }

        [Required(ErrorMessage = "Please select a date")]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "Please select a time")]
        [Display(Name = "Appointment Time")]
        public string AppointmentTime { get; set; } = string.Empty;

        [Display(Name = "Special Requests")]
        [StringLength(500, ErrorMessage = "Special requests cannot exceed 500 characters")]
        public string? SpecialRequests { get; set; }

        [Display(Name = "Booking Status")]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public Service? Service { get; set; }
        public StaffMember? StaffMember { get; set; }

        // Computed properties
        public string FullName => $"{FirstName} {LastName}";
        public DateTime AppointmentDateTime => AppointmentDate.Date.Add(TimeSpan.Parse(AppointmentTime));
        public bool IsUpcoming => AppointmentDateTime > DateTime.Now;
    }

    public enum BookingStatus
    {
        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Confirmed")]
        Confirmed,
        [Display(Name = "Completed")]
        Completed,
        [Display(Name = "Cancelled")]
        Cancelled
    }
} 