using BarberSalonPrototype.Models;

namespace BarberSalonPrototype.Services
{
    public class StaffService : IStaffService
    {
        private readonly List<StaffMember> _staffMembers;
        private readonly ILogger<StaffService> _logger;

        public StaffService(ILogger<StaffService> logger)
        {
            _logger = logger;
            _staffMembers = InitializeStaffData();
        }

        public async Task<IEnumerable<StaffMember>> GetAllStaffAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all staff members");
                return await Task.FromResult(_staffMembers.OrderBy(s => s.SortOrder).ThenBy(s => s.FullName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all staff members");
                throw;
            }
        }

        public async Task<StaffMember?> GetStaffByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving staff member with ID: {Id}", id);
                return await Task.FromResult(_staffMembers.FirstOrDefault(s => s.Id == id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving staff member with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<StaffMember>> GetActiveStaffAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving active staff members");
                return await Task.FromResult(_staffMembers.Where(s => s.IsActive).OrderBy(s => s.SortOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active staff members");
                throw;
            }
        }

        public async Task<IEnumerable<StaffMember>> GetStaffByRoleAsync(string role)
        {
            try
            {
                _logger.LogInformation("Retrieving staff members by role: {Role}", role);
                return await Task.FromResult(_staffMembers.Where(s => s.Role.Equals(role, StringComparison.OrdinalIgnoreCase) && s.IsActive));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving staff members by role: {Role}", role);
                throw;
            }
        }

        public async Task<StaffMember> CreateStaffAsync(StaffMember staffMember)
        {
            try
            {
                _logger.LogInformation("Creating new staff member: {Name}", staffMember.FullName);
                staffMember.Id = _staffMembers.Max(s => s.Id) + 1;
                _staffMembers.Add(staffMember);
                return await Task.FromResult(staffMember);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating staff member: {Name}", staffMember.FullName);
                throw;
            }
        }

        public async Task<StaffMember> UpdateStaffAsync(StaffMember staffMember)
        {
            try
            {
                _logger.LogInformation("Updating staff member: {Name}", staffMember.FullName);
                var existingStaff = _staffMembers.FirstOrDefault(s => s.Id == staffMember.Id);
                if (existingStaff == null)
                {
                    throw new ArgumentException($"Staff member with ID {staffMember.Id} not found");
                }

                var index = _staffMembers.IndexOf(existingStaff);
                _staffMembers[index] = staffMember;
                return await Task.FromResult(staffMember);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating staff member: {Name}", staffMember.FullName);
                throw;
            }
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting staff member with ID: {Id}", id);
                var staffMember = _staffMembers.FirstOrDefault(s => s.Id == id);
                if (staffMember == null)
                {
                    return false;
                }

                return await Task.FromResult(_staffMembers.Remove(staffMember));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting staff member with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<GalleryImage>> GetStaffGalleryAsync(int staffId)
        {
            try
            {
                _logger.LogInformation("Retrieving gallery for staff member ID: {StaffId}", staffId);
                var staffMember = await GetStaffByIdAsync(staffId);
                return await Task.FromResult(staffMember?.Gallery.OrderBy(g => g.SortOrder) ?? Enumerable.Empty<GalleryImage>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving gallery for staff member ID: {StaffId}", staffId);
                throw;
            }
        }

        public async Task<GalleryImage> AddGalleryImageAsync(GalleryImage image)
        {
            try
            {
                _logger.LogInformation("Adding gallery image for staff member ID: {StaffId}", image.StaffMemberId);
                var staffMember = await GetStaffByIdAsync(image.StaffMemberId);
                if (staffMember == null)
                {
                    throw new ArgumentException($"Staff member with ID {image.StaffMemberId} not found");
                }

                image.Id = staffMember.Gallery.Max(g => g.Id) + 1;
                staffMember.Gallery.Add(image);
                return await Task.FromResult(image);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding gallery image for staff member ID: {StaffId}", image.StaffMemberId);
                throw;
            }
        }

        public async Task<bool> RemoveGalleryImageAsync(int imageId)
        {
            try
            {
                _logger.LogInformation("Removing gallery image with ID: {ImageId}", imageId);
                foreach (var staffMember in _staffMembers)
                {
                    var image = staffMember.Gallery.FirstOrDefault(g => g.Id == imageId);
                    if (image != null)
                    {
                        return await Task.FromResult(staffMember.Gallery.Remove(image));
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing gallery image with ID: {ImageId}", imageId);
                throw;
            }
        }

        private List<StaffMember> InitializeStaffData()
        {
            return new List<StaffMember>
            {
                new StaffMember
                {
                    Id = 1,
                    FullName = "Michael Rodriguez",
                    Role = "Master Barber",
                    Description = "With over 15 years of experience, Michael specializes in classic cuts, fades, and modern styling. His attention to detail and passion for the craft makes him a favorite among our clients.",
                    ImageUrl = "images/staff/Michael-Rodriguez.png",
                    Specializations = new List<string> { "Classic Cuts", "Fades", "Beard Trimming", "Modern Styling" },
                    YearsOfExperience = 15,
                    InstagramUrl = "https://instagram.com/michaelbarber",
                    FacebookUrl = "https://facebook.com/michaelbarber",
                    TikTokUrl = "https://tiktok.com/@michaelbarber",
                    SortOrder = 1,
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 1, StaffMemberId = 1, ImageUrl = "images/gallery/michael-1.jpg", Caption = "Classic Pompadour", SortOrder = 1 },
                        new GalleryImage { Id = 2, StaffMemberId = 1, ImageUrl = "images/gallery/michael-2.jpg", Caption = "Modern Fade", SortOrder = 2 },
                        new GalleryImage { Id = 3, StaffMemberId = 1, ImageUrl = "images/gallery/michael-3.jpg", Caption = "Beard Styling", SortOrder = 3 }
                    }
                },
                new StaffMember
                {
                    Id = 2,
                    FullName = "Sarah Johnson",
                    Role = "Hair Stylist",
                    Description = "Sarah brings creativity and innovation to every haircut. She excels in color treatments, highlights, and creating unique styles that reflect each client's personality.",
                    ImageUrl = "images/staff/Sarah-Johnson.png",
                    Specializations = new List<string> { "Color Treatments", "Highlights", "Balayage", "Styling" },
                    YearsOfExperience = 8,
                    InstagramUrl = "https://instagram.com/sarahstylist",
                    FacebookUrl = "https://facebook.com/sarahstylist",
                    TikTokUrl = "https://tiktok.com/@sarahstylist",
                    SortOrder = 2,
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 4, StaffMemberId = 2, ImageUrl = "images/gallery/sarah-1.jpg", Caption = "Balayage Highlights", SortOrder = 1 },
                        new GalleryImage { Id = 5, StaffMemberId = 2, ImageUrl = "images/gallery/sarah-2.jpg", Caption = "Color Transformation", SortOrder = 2 },
                        new GalleryImage { Id = 6, StaffMemberId = 2, ImageUrl = "images/gallery/sarah-3.jpg", Caption = "Styling Session", SortOrder = 3 }
                    }
                },
                new StaffMember
                {
                    Id = 3,
                    FullName = "David Chen",
                    Role = "Barber",
                    Description = "David is known for his precision cuts and ability to create clean, sharp lines. He specializes in contemporary styles and traditional barbering techniques.",
                    ImageUrl = "images/staff/David-Chen.png",
                    Specializations = new List<string> { "Precision Cuts", "Line-ups", "Contemporary Styles", "Traditional Techniques" },
                    YearsOfExperience = 12,
                    InstagramUrl = "https://instagram.com/davidbarber",
                    FacebookUrl = "https://facebook.com/davidbarber",
                    TikTokUrl = "https://tiktok.com/@davidbarber",
                    SortOrder = 3,
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 7, StaffMemberId = 3, ImageUrl = "images/gallery/david-1.jpg", Caption = "Precision Line-up", SortOrder = 1 },
                        new GalleryImage { Id = 8, StaffMemberId = 3, ImageUrl = "images/gallery/david-2.jpg", Caption = "Contemporary Cut", SortOrder = 2 },
                        new GalleryImage { Id = 9, StaffMemberId = 3, ImageUrl = "images/gallery/david-3.jpg", Caption = "Traditional Style", SortOrder = 3 }
                    }
                },
                new StaffMember
                {
                    Id = 4,
                    FullName = "Emily Martinez",
                    Role = "Nail Technician",
                    Description = "Emily creates stunning nail art and provides exceptional nail care services. Her attention to detail and artistic flair make every manicure and pedicure a work of art.",
                    ImageUrl = "images/staff/Emily-Martinez.png",
                    Specializations = new List<string> { "Nail Art", "Gel Manicures", "Acrylic Nails", "Nail Care" },
                    YearsOfExperience = 6,
                    InstagramUrl = "https://instagram.com/emilynails",
                    FacebookUrl = "https://facebook.com/emilynails",
                    TikTokUrl = "https://tiktok.com/@emilynails",
                    SortOrder = 4,
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 10, StaffMemberId = 4, ImageUrl = "images/gallery/emily-1.jpg", Caption = "Floral Nail Art", SortOrder = 1 },
                        new GalleryImage { Id = 11, StaffMemberId = 4, ImageUrl = "images/gallery/emily-2.jpg", Caption = "Gel Manicure", SortOrder = 2 },
                        new GalleryImage { Id = 12, StaffMemberId = 4, ImageUrl = "images/gallery/emily-3.jpg", Caption = "Acrylic Design", SortOrder = 3 }
                    }
                },
                new StaffMember
                {
                    Id = 5,
                    FullName = "Alex Thompson",
                    Role = "Skin Care Specialist",
                    Description = "Alex is passionate about helping clients achieve healthy, glowing skin. She offers personalized treatments and expert advice on skincare routines.",
                    ImageUrl = "images/staff/Alex-Thompson.png",
                    Specializations = new List<string> { "Facial Treatments", "Skin Analysis", "Anti-aging", "Acne Treatment" },
                    YearsOfExperience = 10,
                    InstagramUrl = "https://instagram.com/alexskincare",
                    FacebookUrl = "https://facebook.com/alexskincare",
                    TikTokUrl = "https://tiktok.com/@alexskincare",
                    SortOrder = 5,
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 13, StaffMemberId = 5, ImageUrl = "images/gallery/alex-1.jpg", Caption = "Facial Treatment", SortOrder = 1 },
                        new GalleryImage { Id = 14, StaffMemberId = 5, ImageUrl = "images/gallery/alex-2.jpg", Caption = "Skin Analysis", SortOrder = 2 },
                        new GalleryImage { Id = 15, StaffMemberId = 5, ImageUrl = "images/gallery/alex-3.jpg", Caption = "Anti-aging Treatment", SortOrder = 3 }
                    }
                },
                new StaffMember
                {
                    Id = 6,
                    FullName = "Jessica Lee",
                    Role = "Makeup Artist",
                    Description = "Jessica is a creative makeup artist with a passion for enhancing natural beauty. She specializes in bridal, event, and editorial makeup, ensuring every client looks and feels their best.",
                    ImageUrl = "images/staff/Jessica-Lee.png", // Add this image to wwwroot/images/staff/
                    Specializations = new List<string> { "Bridal Makeup", "Event Makeup", "Editorial Makeup", "Natural Looks", "Makeup Lessons" },
                    YearsOfExperience = 7,
                    InstagramUrl = "https://instagram.com/jessicamakeup",
                    FacebookUrl = "https://facebook.com/jessicamakeup",
                    TikTokUrl = "https://tiktok.com/@jessicamakeup",
                    SortOrder = 6,
                    Gallery = new List<GalleryImage>
                    {
                        new GalleryImage { Id = 16, StaffMemberId = 6, ImageUrl = "images/gallery/jessica-1.jpg", Caption = "Bridal Look", SortOrder = 1 },
                        new GalleryImage { Id = 17, StaffMemberId = 6, ImageUrl = "images/gallery/jessica-2.jpg", Caption = "Editorial Glam", SortOrder = 2 },
                        new GalleryImage { Id = 18, StaffMemberId = 6, ImageUrl = "images/gallery/jessica-3.jpg", Caption = "Natural Beauty", SortOrder = 3 }
                    }
                }
            };
        }
    }
} 