using System.ComponentModel.DataAnnotations;

namespace BarberSalonPrototype.Models
{
    public class StaffSpecialization
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Foreign key
        public int StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; } = null!;
    }
} 