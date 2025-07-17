using BarberSalonPrototype.Models;
using Microsoft.AspNetCore.Identity;

namespace BarberSalonPrototype.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed roles first
            await SeedRolesAsync(roleManager);

            // Seed admin user
            await SeedAdminUserAsync(userManager);

            // Check if we have any data already
            if (context.Services.Any())
            {
                return; // Database has been seeded
            }

            // Seed Services
            var services = GetInitialServices();
            context.Services.AddRange(services);

            // Seed Staff Members
            var staffMembers = GetInitialStaffMembers();
            context.StaffMembers.AddRange(staffMembers);

            await context.SaveChangesAsync();

            // Seed Gallery Images (after staff are saved to get IDs)
            var galleryImages = GetInitialGalleryImages();
            context.GalleryImages.AddRange(galleryImages);

            await context.SaveChangesAsync();
        }

        private static List<Service> GetInitialServices()
        {
            return new List<Service>
            {
                // Hair Services - Men
                new Service { Name = "Taper Fade", Description = "Classic taper fade with clean graduation from long to short hair", Price = "R150", DurationMinutes = 45, Category = ServiceCategory.HairServicesMen, IsPopular = true, ImageUrl = "/images/services/Taper-Fade.jpg", SortOrder = 1 },
                new Service { Name = "Skin Fade / Bald Fade", Description = "Ultra-short fade cut down to the skin for a sharp, clean look", Price = "R180", DurationMinutes = 50, Category = ServiceCategory.HairServicesMen, IsPopular = true, ImageUrl = "/images/services/skin-fade_bald-fade.jpg", SortOrder = 2 },
                new Service { Name = "Low / Mid / High Fade", Description = "Professional fade cuts at varying heights to suit your style preference", Price = "R160", DurationMinutes = 45, Category = ServiceCategory.HairServicesMen, IsPopular = true, ImageUrl = "/images/services/Low-Mid -High-Fade.jpg", SortOrder = 3 },
                new Service { Name = "Beard Trim & Line-Up", Description = "Professional beard trimming with clean line-up for a polished look", Price = "R100", DurationMinutes = 30, Category = ServiceCategory.HairServicesMen, IsPopular = true, ImageUrl = "/images/services/Beard-Trim_and-Line-Up.jpg", SortOrder = 6 },
                
                // Hair Services - Women
                new Service { Name = "Wash, Blow & Style", Description = "Professional wash, blow-dry and styling service", Price = "R120", DurationMinutes = 60, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Wash-Blow-and-Style.png", SortOrder = 1 },
                new Service { Name = "Silk Press", Description = "Heat styling for smooth, silky straight hair with natural movement", Price = "R280", DurationMinutes = 90, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Silk-Press.png", SortOrder = 2 },
                
                // Nail Services
                new Service { Name = "Gel Manicure", Description = "Long-lasting gel manicure with professional finish", Price = "R180", DurationMinutes = 60, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/gel-manicure.jpg", SortOrder = 1 },
                new Service { Name = "Pedicure", Description = "Relaxing pedicure service with foot massage", Price = "R150", DurationMinutes = 60, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/pedicure.jpg", SortOrder = 2 }
            };
        }

        private static List<StaffMember> GetInitialStaffMembers()
        {
            return new List<StaffMember>
            {
                new StaffMember
                {
                    FullName = "Michael Rodriguez",
                    Role = "Master Barber",
                    Description = "With over 15 years of experience, Michael specializes in classic cuts, fades, and modern styling. His attention to detail and passion for the craft makes him a favorite among our clients.",
                    ImageUrl = "/images/staff/Michael-Rodriguez.png",
                    Specializations = new List<StaffSpecialization> {
                        new StaffSpecialization { Name = "Classic Cuts" },
                        new StaffSpecialization { Name = "Fades" },
                        new StaffSpecialization { Name = "Beard Trimming" },
                        new StaffSpecialization { Name = "Modern Styling" }
                    },
                    YearsOfExperience = 15,
                    InstagramUrl = "https://instagram.com/michaelbarber",
                    FacebookUrl = "https://facebook.com/michaelbarber",
                    TikTokUrl = "https://tiktok.com/@michaelbarber",
                    SortOrder = 1
                },
                new StaffMember
                {
                    FullName = "Sarah Johnson",
                    Role = "Hair Stylist",
                    Description = "Sarah brings creativity and innovation to every haircut. She excels in color treatments, highlights, and creating unique styles that reflect each client's personality.",
                    ImageUrl = "/images/staff/Sarah-Johnson.png",
                    Specializations = new List<StaffSpecialization> {
                        new StaffSpecialization { Name = "Color Treatments" },
                        new StaffSpecialization { Name = "Highlights" },
                        new StaffSpecialization { Name = "Balayage" },
                        new StaffSpecialization { Name = "Styling" }
                    },
                    YearsOfExperience = 8,
                    InstagramUrl = "https://instagram.com/sarahstylist",
                    FacebookUrl = "https://facebook.com/sarahstylist",
                    TikTokUrl = "https://tiktok.com/@sarahstylist",
                    SortOrder = 2
                },
                new StaffMember
                {
                    FullName = "David Chen",
                    Role = "Barber",
                    Description = "David is known for his precision cuts and ability to create clean, sharp lines. He specializes in contemporary styles and traditional barbering techniques.",
                    ImageUrl = "/images/staff/David-Chen.png",
                    Specializations = new List<StaffSpecialization> {
                        new StaffSpecialization { Name = "Precision Cuts" },
                        new StaffSpecialization { Name = "Line-ups" },
                        new StaffSpecialization { Name = "Contemporary Styles" },
                        new StaffSpecialization { Name = "Traditional Techniques" }
                    },
                    YearsOfExperience = 12,
                    InstagramUrl = "https://instagram.com/davidbarber",
                    FacebookUrl = "https://facebook.com/davidbarber",
                    TikTokUrl = "https://tiktok.com/@davidbarber",
                    SortOrder = 3
                }
            };
        }

        private static List<GalleryImage> GetInitialGalleryImages()
        {
            return new List<GalleryImage>
            {
                new GalleryImage { StaffMemberId = 1, ImageUrl = "/images/gallery/michael-1.jpg", Caption = "Classic Pompadour", SortOrder = 1 },
                new GalleryImage { StaffMemberId = 1, ImageUrl = "/images/gallery/michael-2.jpg", Caption = "Modern Fade", SortOrder = 2 },
                new GalleryImage { StaffMemberId = 2, ImageUrl = "/images/gallery/sarah-1.jpg", Caption = "Balayage Highlights", SortOrder = 1 },
                new GalleryImage { StaffMemberId = 2, ImageUrl = "/images/gallery/sarah-2.jpg", Caption = "Color Transformation", SortOrder = 2 },
                new GalleryImage { StaffMemberId = 3, ImageUrl = "/images/gallery/david-1.jpg", Caption = "Precision Line-up", SortOrder = 1 },
                new GalleryImage { StaffMemberId = 3, ImageUrl = "/images/gallery/david-2.jpg", Caption = "Contemporary Cut", SortOrder = 2 }
            };
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Staff", "Customer" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@barbersalon.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User",
                    IsActive = true,
                    DateJoined = DateTime.Now
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}