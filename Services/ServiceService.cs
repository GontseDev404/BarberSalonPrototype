using BarberSalonPrototype.Models;

namespace BarberSalonPrototype.Services
{
    public class ServiceService : IServiceService
    {
        private readonly List<Service> _services;
        private readonly ILogger<ServiceService> _logger;

        public ServiceService(ILogger<ServiceService> logger)
        {
            _logger = logger;
            _services = InitializeServiceData();
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all services");
                return await Task.FromResult(_services.OrderBy(s => s.Category).ThenBy(s => s.SortOrder).ThenBy(s => s.Name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all services");
                throw;
            }
        }

        public async Task<Service?> GetServiceByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving service with ID: {Id}", id);
                return await Task.FromResult(_services.FirstOrDefault(s => s.Id == id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Service>> GetServicesByCategoryAsync(ServiceCategory category)
        {
            try
            {
                _logger.LogInformation("Retrieving services by category: {Category}", category);
                return await Task.FromResult(_services.Where(s => s.Category == category).OrderBy(s => s.SortOrder).ThenBy(s => s.Name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving services by category: {Category}", category);
                throw;
            }
        }

        public async Task<IEnumerable<Service>> GetPopularServicesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving popular services");
                return await Task.FromResult(_services.Where(s => s.IsPopular).OrderBy(s => s.SortOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving popular services");
                throw;
            }
        }

        public async Task<Service> CreateServiceAsync(Service service)
        {
            try
            {
                _logger.LogInformation("Creating new service: {Name}", service.Name);
                service.Id = _services.Max(s => s.Id) + 1;
                _services.Add(service);
                return await Task.FromResult(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating service: {Name}", service.Name);
                throw;
            }
        }

        public async Task<Service> UpdateServiceAsync(Service service)
        {
            try
            {
                _logger.LogInformation("Updating service: {Name}", service.Name);
                var existingService = _services.FirstOrDefault(s => s.Id == service.Id);
                if (existingService == null)
                {
                    throw new ArgumentException($"Service with ID {service.Id} not found");
                }

                var index = _services.IndexOf(existingService);
                _services[index] = service;
                return await Task.FromResult(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service: {Name}", service.Name);
                throw;
            }
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting service with ID: {Id}", id);
                var service = _services.FirstOrDefault(s => s.Id == id);
                if (service == null)
                {
                    return false;
                }

                return await Task.FromResult(_services.Remove(service));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting service with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<ServiceCategory>> GetServiceCategoriesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving service categories");
                return await Task.FromResult(Enum.GetValues<ServiceCategory>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service categories");
                throw;
            }
        }

        private List<Service> InitializeServiceData()
        {
            return new List<Service>
            {
                // Hair Services - Men
                new Service { Id = 1, Name = "Taper Fade", Description = "Classic taper fade with clean graduation from long to short hair", Price = "R150", DurationMinutes = 45, Category = ServiceCategory.HairServicesMen, IsPopular = true, ImageUrl = "/images/services/taper-fade.jpg", SortOrder = 1 },
                new Service { Id = 2, Name = "Skin Fade / Bald Fade", Description = "Ultra-short fade cut down to the skin for a sharp, clean look", Price = "R180", DurationMinutes = 50, Category = ServiceCategory.HairServicesMen, IsPopular = true, ImageUrl = "/images/services/skin-fade_bald-fade.jpg", SortOrder = 2 },
                new Service { Id = 3, Name = "Low / Mid / High Fade", Description = "Professional fade cuts at varying heights to suit your style preference", Price = "R160", DurationMinutes = 45, Category = ServiceCategory.HairServicesMen, IsPopular = true, ImageUrl = "/images/services/Low-Mid -High-Fade.jpg", SortOrder = 3 },
                new Service { Id = 4, Name = "Afro Cut & Shape", Description = "Specialized cutting and shaping for natural African hair textures", Price = "R140", DurationMinutes = 40, Category = ServiceCategory.HairServicesMen, IsPopular = false, ImageUrl = "/images/services/Afro-Cut-Shape.jpg", SortOrder = 4 },
                new Service { Id = 5, Name = "Waves Maintenance & Cut", Description = "Professional wave pattern maintenance with precision cutting", Price = "R170", DurationMinutes = 50, Category = ServiceCategory.HairServicesMen, IsPopular = false, ImageUrl = "/images/services/Waves-Maintenance-Cut.png", SortOrder = 5 },
                new Service { Id = 6, Name = "Beard Trim & Line-Up", Description = "Professional beard trimming with clean line-up for a polished look", Price = "R100", DurationMinutes = 30, Category = ServiceCategory.HairServicesMen, IsPopular = true, ImageUrl = "/images/services/Beard-Trim_and-Line-Up.jpg", SortOrder = 6 },
                new Service { Id = 7, Name = "Hot Towel Shave", Description = "Traditional hot towel shave for the ultimate smooth finish", Price = "R120", DurationMinutes = 35, Category = ServiceCategory.HairServicesMen, IsPopular = false, ImageUrl = "/images/services/hot-towel-shave.jpg", SortOrder = 7 },
                new Service { Id = 8, Name = "Hairline Enhancement", Description = "Precision hairline cleanup and enhancement for a fresh appearance", Price = "R80", DurationMinutes = 25, Category = ServiceCategory.HairServicesMen, IsPopular = false, ImageUrl = "/images/services/hairline-enhancement.png", SortOrder = 8 },
                new Service { Id = 9, Name = "Dreadlock Retwist", Description = "Professional dreadlock maintenance and retwisting service", Price = "R200", DurationMinutes = 90, Category = ServiceCategory.HairServicesMen, IsPopular = false, ImageUrl = "/images/services/dreadlock-retwist.jpg", SortOrder = 9 },
                new Service { Id = 10, Name = "Hair & Scalp Treatment", Description = "Deep conditioning treatment for healthy hair and scalp", Price = "R150", DurationMinutes = 45, Category = ServiceCategory.HairServicesMen, IsPopular = false, ImageUrl = "/images/services/hair-scalp-treatment.jpg", SortOrder = 10 },
                new Service { Id = 11, Name = "Hair Dye for Men", Description = "Professional hair coloring services for men", Price = "R250", DurationMinutes = 75, Category = ServiceCategory.HairServicesMen, IsPopular = false, ImageUrl = "/images/services/Hair-Dye-for-Men.png", SortOrder = 11 },
                new Service { Id = 12, Name = "Cornrows / Braiding (Men)", Description = "Traditional cornrow braiding and protective styling for men", Price = "R180", DurationMinutes = 120, Category = ServiceCategory.HairServicesMen, IsPopular = false, ImageUrl = "/images/services/Cornrows -Braiding-(Men).png", SortOrder = 12 },

                // Hair Services - Women
                new Service { Id = 13, Name = "Wash, Blow & Style", Description = "Professional wash, blow-dry and styling service", Price = "R120", DurationMinutes = 60, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Wash-Blow-and-Style.png", SortOrder = 1 },
                new Service { Id = 14, Name = "Silk Press", Description = "Heat styling for smooth, silky straight hair with natural movement", Price = "R280", DurationMinutes = 90, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Silk-Press.png", SortOrder = 2 },
                new Service { Id = 15, Name = "Hair Relaxing", Description = "Chemical relaxing treatment for permanently straightened hair", Price = "R320", DurationMinutes = 120, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Hair-Relaxing.png", SortOrder = 3 },
                new Service { Id = 16, Name = "Cornrows / Feed-in Braids", Description = "Protective braiding styles including feed-in techniques", Price = "R250", DurationMinutes = 180, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Cornrows-Feed-in-Braids.png", SortOrder = 4 },
                new Service { Id = 17, Name = "Box Braids / Knotless Braids", Description = "Long-lasting protective box braids and knotless variations", Price = "R450", DurationMinutes = 300, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Box-Braids-Knotless-Braids.png", SortOrder = 5 },
                new Service { Id = 18, Name = "Twist Styles", Description = "Two-strand twists, Marley twists and other protective twist styles", Price = "R300", DurationMinutes = 240, Category = ServiceCategory.HairServicesWomen, IsPopular = false, ImageUrl = "/images/services/Twist-Styles.png", SortOrder = 6 },
                new Service { Id = 19, Name = "Frontal / Closure Wig Install", Description = "Professional wig installation with frontal or closure pieces", Price = "R400", DurationMinutes = 180, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Frontal-Closure-Wig-Install.png", SortOrder = 7 },
                new Service { Id = 20, Name = "Weave Install", Description = "Professional hair weave installation service", Price = "R350", DurationMinutes = 150, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Weave-Install.png", SortOrder = 8 },
                new Service { Id = 21, Name = "Wig Revamp / Styling", Description = "Wig restoration, restyling and customization service", Price = "R180", DurationMinutes = 90, Category = ServiceCategory.HairServicesWomen, IsPopular = false, ImageUrl = "/images/services/Wig-Revamp-Styling.png", SortOrder = 9 },
                new Service { Id = 22, Name = "Colouring & Highlights", Description = "Professional hair coloring and highlight services", Price = "R380", DurationMinutes = 150, Category = ServiceCategory.HairServicesWomen, IsPopular = true, ImageUrl = "/images/services/Colouring-and-Highlights.png", SortOrder = 10 },
                new Service { Id = 23, Name = "Hair Treatment / Repair Mask", Description = "Deep conditioning and repair treatment for damaged hair", Price = "R200", DurationMinutes = 60, Category = ServiceCategory.HairServicesWomen, IsPopular = false, ImageUrl = "/images/services/Hair-Treatment-Repair-Mask.png", SortOrder = 11 },
                new Service { Id = 24, Name = "Trim & Straighten", Description = "Hair trimming with straightening service", Price = "R150", DurationMinutes = 75, Category = ServiceCategory.HairServicesWomen, IsPopular = false, ImageUrl = "/images/services/Trim-and-Straighten.png", SortOrder = 12 },

                // Nail Services
                new Service { Id = 25, Name = "Gel Overlay", Description = "Protective gel overlay for natural nail strengthening", Price = "R180", DurationMinutes = 60, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/gel-overlay.jpg", SortOrder = 1 },
                new Service { Id = 26, Name = "Acrylic Tips", Description = "Classic acrylic nail extensions with tips", Price = "R220", DurationMinutes = 90, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/acrylic-tips.jpg", SortOrder = 2 },
                new Service { Id = 27, Name = "Full Cover Tips", Description = "Full coverage acrylic tips for length and strength", Price = "R250", DurationMinutes = 90, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/full-cover-tips.jpg", SortOrder = 3 },
                new Service { Id = 28, Name = "Nail Art / Designs", Description = "Custom nail art and creative designs", Price = "R80", DurationMinutes = 30, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/nail-art.jpg", SortOrder = 4 },
                new Service { Id = 29, Name = "Sculpted Acrylic", Description = "Hand-sculpted acrylic nails without tips", Price = "R280", DurationMinutes = 105, Category = ServiceCategory.NailServices, IsPopular = false, ImageUrl = "/images/services/sculpted-acrylic.jpg", SortOrder = 5 },
                new Service { Id = 30, Name = "Builder Gel / BIAB", Description = "Builder gel application for natural nail enhancement", Price = "R200", DurationMinutes = 75, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/builder-gel.jpg", SortOrder = 6 },
                new Service { Id = 31, Name = "Manicure (Basic / Spa)", Description = "Professional manicure service - basic or spa treatment", Price = "R120", DurationMinutes = 45, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/manicure.jpg", SortOrder = 7 },
                new Service { Id = 32, Name = "Pedicure (Basic / Spa)", Description = "Relaxing pedicure service - basic or spa treatment", Price = "R150", DurationMinutes = 60, Category = ServiceCategory.NailServices, IsPopular = true, ImageUrl = "/images/services/pedicure.jpg", SortOrder = 8 },
                new Service { Id = 33, Name = "Polish Change", Description = "Quick nail polish change service", Price = "R60", DurationMinutes = 20, Category = ServiceCategory.NailServices, IsPopular = false, ImageUrl = "/images/services/polish-change.jpg", SortOrder = 9 },
                new Service { Id = 34, Name = "Soak-off / Nail Removal", Description = "Safe removal of artificial nails and polish", Price = "R80", DurationMinutes = 30, Category = ServiceCategory.NailServices, IsPopular = false, ImageUrl = "/images/services/nail-removal.jpg", SortOrder = 10 },
                new Service { Id = 35, Name = "Nail Repair", Description = "Professional nail repair for broken or damaged nails", Price = "R50", DurationMinutes = 20, Category = ServiceCategory.NailServices, IsPopular = false, ImageUrl = "/images/services/nail-repair.jpg", SortOrder = 11 },
                new Service { Id = 36, Name = "Cuticle Care & Oil Treatment", Description = "Cuticle maintenance and nourishing oil treatment", Price = "R40", DurationMinutes = 15, Category = ServiceCategory.NailServices, IsPopular = false, ImageUrl = "/images/services/cuticle-care.jpg", SortOrder = 12 },

                // Skin & Beauty Services
                new Service { Id = 37, Name = "Deep Clean Facial", Description = "Deep cleansing facial for congested and problem skin", Price = "R180", DurationMinutes = 60, Category = ServiceCategory.SkinBeautyServices, IsPopular = true, ImageUrl = "/images/services/deep-clean-facial.jpg", SortOrder = 1 },
                new Service { Id = 38, Name = "Hydrating Facial", Description = "Moisturizing facial treatment for dry and dehydrated skin", Price = "R170", DurationMinutes = 60, Category = ServiceCategory.SkinBeautyServices, IsPopular = true, ImageUrl = "/images/services/hydrating-facial.jpg", SortOrder = 2 },
                new Service { Id = 39, Name = "Anti-Acne Facial", Description = "Specialized facial treatment for acne-prone skin", Price = "R200", DurationMinutes = 75, Category = ServiceCategory.SkinBeautyServices, IsPopular = true, ImageUrl = "/images/services/anti-acne-facial.jpg", SortOrder = 3 },
                new Service { Id = 40, Name = "Eyebrow Wax / Threading", Description = "Professional eyebrow shaping by wax or threading", Price = "R80", DurationMinutes = 20, Category = ServiceCategory.SkinBeautyServices, IsPopular = true, ImageUrl = "/images/services/eyebrow-wax.jpg", SortOrder = 4 },
                new Service { Id = 41, Name = "Eyebrow Tinting", Description = "Eyebrow tinting for enhanced definition and color", Price = "R60", DurationMinutes = 15, Category = ServiceCategory.SkinBeautyServices, IsPopular = false, ImageUrl = "/images/services/eyebrow-tinting.jpg", SortOrder = 5 },
                new Service { Id = 42, Name = "Eyelash Extensions", Description = "Individual eyelash extensions for fuller, longer lashes", Price = "R280", DurationMinutes = 120, Category = ServiceCategory.SkinBeautyServices, IsPopular = true, ImageUrl = "/images/services/eyelash-extensions.jpg", SortOrder = 6 },
                new Service { Id = 43, Name = "Eyelash Lift & Tint", Description = "Lash lifting and tinting for natural enhancement", Price = "R150", DurationMinutes = 45, Category = ServiceCategory.SkinBeautyServices, IsPopular = true, ImageUrl = "/images/services/eyelash-lift.jpg", SortOrder = 7 },
                new Service { Id = 44, Name = "Makeup Application", Description = "Professional makeup application for events and occasions", Price = "R200", DurationMinutes = 60, Category = ServiceCategory.SkinBeautyServices, IsPopular = true, ImageUrl = "/images/services/makeup-application.jpg", SortOrder = 8 },
                new Service { Id = 45, Name = "Skin Peels / Exfoliation", Description = "Chemical peels and exfoliation treatments for skin renewal", Price = "R220", DurationMinutes = 45, Category = ServiceCategory.SkinBeautyServices, IsPopular = false, ImageUrl = "/images/services/skin-peels.jpg", SortOrder = 9 },
                new Service { Id = 46, Name = "Derma-planing", Description = "Professional derma-planing for smooth, radiant skin", Price = "R180", DurationMinutes = 30, Category = ServiceCategory.SkinBeautyServices, IsPopular = false, ImageUrl = "/images/services/dermaplaning.jpg", SortOrder = 10 },
                new Service { Id = 47, Name = "Beard Facial / Detox (Men)", Description = "Specialized facial treatment for men's beard and skin care", Price = "R150", DurationMinutes = 45, Category = ServiceCategory.SkinBeautyServices, IsPopular = false, ImageUrl = "/images/services/beard-facial.jpg", SortOrder = 11 },

                // Add-ons & Extras
                new Service { Id = 48, Name = "Scalp Massage", Description = "Relaxing scalp massage for stress relief and circulation", Price = "R50", DurationMinutes = 15, Category = ServiceCategory.AddonsExtras, IsPopular = false, ImageUrl = "/images/services/scalp-massage.jpg", SortOrder = 1 },
                new Service { Id = 49, Name = "Nail Art Add-on", Description = "Additional nail art service to complement main treatment", Price = "R30", DurationMinutes = 15, Category = ServiceCategory.AddonsExtras, IsPopular = true, ImageUrl = "/images/services/nail-art-addon.jpg", SortOrder = 2 },
                new Service { Id = 50, Name = "Hair Wash Only", Description = "Professional hair wash and conditioning service", Price = "R40", DurationMinutes = 20, Category = ServiceCategory.AddonsExtras, IsPopular = false, ImageUrl = "/images/services/hair-wash-only.jpg", SortOrder = 3 },
                new Service { Id = 51, Name = "Express Touch-Up", Description = "Quick hairline or beard touch-up service", Price = "R60", DurationMinutes = 15, Category = ServiceCategory.AddonsExtras, IsPopular = true, ImageUrl = "/images/services/express-touchup.jpg", SortOrder = 4 },
                new Service { Id = 52, Name = "Kids Cuts / Styles", Description = "Fun and comfortable haircuts and styling for children", Price = "R90", DurationMinutes = 30, Category = ServiceCategory.AddonsExtras, IsPopular = true, ImageUrl = "/images/services/kids-cuts.jpg", SortOrder = 5 },
                new Service { Id = 53, Name = "Wig Customization", Description = "Custom wig cutting, styling and personalization", Price = "R150", DurationMinutes = 60, Category = ServiceCategory.AddonsExtras, IsPopular = false, ImageUrl = "/images/services/wig-customization.jpg", SortOrder = 6 }
            };
        }
    }
} 