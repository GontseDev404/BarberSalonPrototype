using System.ComponentModel.DataAnnotations;

namespace BarberSalonPrototype.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters")]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        [Display(Name = "Message")]
        public string Message { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Preferred Contact Method")]
        public ContactMethod PreferredContactMethod { get; set; } = ContactMethod.Email;

        [Display(Name = "Message Type")]
        public MessageType MessageType { get; set; } = MessageType.General;

        [Display(Name = "Is Urgent")]
        public bool IsUrgent { get; set; } = false;

        [Display(Name = "Submitted Date")]
        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        [Display(Name = "Is Read")]
        public bool IsRead { get; set; } = false;

        [Display(Name = "Response Date")]
        public DateTime? ResponseDate { get; set; }
    }

    public enum ContactMethod
    {
        [Display(Name = "Email")]
        Email,
        [Display(Name = "Phone")]
        Phone,
        [Display(Name = "SMS")]
        SMS
    }

    public enum MessageType
    {
        [Display(Name = "General Inquiry")]
        General,
        [Display(Name = "Booking Question")]
        Booking,
        [Display(Name = "Service Inquiry")]
        Service,
        [Display(Name = "Complaint")]
        Complaint,
        [Display(Name = "Feedback")]
        Feedback,
        [Display(Name = "Partnership")]
        Partnership
    }
} 