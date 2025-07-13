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
                return await Task.FromResult(_services.OrderBy(s => s.SortOrder).ThenBy(s => s.Name));
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
                return await Task.FromResult(_services.Where(s => s.Category == category).OrderBy(s => s.SortOrder));
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
                new Service
                {
                    Id = 1,
                    Name = "Fade Cut",
                    Description = "Classic fade haircut with clean lines and modern styling. Perfect for a sharp, professional look.",
                    Price = "R120",
                    DurationMinutes = 45,
                    Category = ServiceCategory.Haircut,
                    IsPopular = true,
                    ImageUrl = "/images/services/fade-cut.jpg",
                    SortOrder = 1
                },
                new Service
                {
                    Id = 2,
                    Name = "Beard Trim",
                    Description = "Professional beard trimming and shaping service. Keep your beard looking sharp and well-groomed.",
                    Price = "R80",
                    DurationMinutes = 30,
                    Category = ServiceCategory.BeardGrooming,
                    IsPopular = true,
                    ImageUrl = "/images/services/beard-trim.jpg",
                    SortOrder = 2
                },
                new Service
                {
                    Id = 3,
                    Name = "Haircut & Style",
                    Description = "Complete haircut with professional styling. Includes consultation, cut, wash, and style.",
                    Price = "R150",
                    DurationMinutes = 60,
                    Category = ServiceCategory.Haircut,
                    IsPopular = true,
                    ImageUrl = "/images/services/haircut-style.jpg",
                    SortOrder = 3
                },
                new Service
                {
                    Id = 4,
                    Name = "Kids Haircut",
                    Description = "Specialized haircuts for children under 12. Fun and comfortable experience for young clients.",
                    Price = "R90",
                    DurationMinutes = 30,
                    Category = ServiceCategory.Haircut,
                    IsPopular = false,
                    ImageUrl = "/images/services/kids-haircut.jpg",
                    SortOrder = 4
                },
                new Service
                {
                    Id = 5,
                    Name = "Hair Color",
                    Description = "Professional hair coloring and highlights. Transform your look with expert color services.",
                    Price = "R200",
                    DurationMinutes = 120,
                    Category = ServiceCategory.HairTreatment,
                    IsPopular = false,
                    ImageUrl = "/images/services/hair-color.jpg",
                    SortOrder = 5
                },
                new Service
                {
                    Id = 6,
                    Name = "Hair Treatment",
                    Description = "Deep conditioning and hair treatment. Restore and nourish your hair with premium products.",
                    Price = "R180",
                    DurationMinutes = 45,
                    Category = ServiceCategory.HairTreatment,
                    IsPopular = false,
                    ImageUrl = "/images/services/hair-treatment.jpg",
                    SortOrder = 6
                },
                new Service
                {
                    Id = 7,
                    Name = "Manicure",
                    Description = "Classic manicure with nail care. Includes nail shaping, cuticle care, and polish application.",
                    Price = "R120",
                    DurationMinutes = 45,
                    Category = ServiceCategory.NailCare,
                    IsPopular = false,
                    ImageUrl = "/images/services/manicure.jpg",
                    SortOrder = 7
                },
                new Service
                {
                    Id = 8,
                    Name = "Pedicure",
                    Description = "Relaxing pedicure with foot care. Complete foot treatment including exfoliation and massage.",
                    Price = "R150",
                    DurationMinutes = 60,
                    Category = ServiceCategory.NailCare,
                    IsPopular = false,
                    ImageUrl = "/images/services/pedicure.jpg",
                    SortOrder = 8
                },
                new Service
                {
                    Id = 9,
                    Name = "Facial",
                    Description = "Rejuvenating facial treatment. Deep cleansing and skin rejuvenation for a fresh, glowing complexion.",
                    Price = "R250",
                    DurationMinutes = 75,
                    Category = ServiceCategory.FacialSkincare,
                    IsPopular = false,
                    ImageUrl = "/images/services/facial.jpg",
                    SortOrder = 9
                }
            };
        }
    }
} 