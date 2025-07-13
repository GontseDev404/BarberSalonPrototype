using BarberSalonPrototype.Models;

namespace BarberSalonPrototype.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service?> GetServiceByIdAsync(int id);
        Task<IEnumerable<Service>> GetServicesByCategoryAsync(ServiceCategory category);
        Task<IEnumerable<Service>> GetPopularServicesAsync();
        Task<Service> CreateServiceAsync(Service service);
        Task<Service> UpdateServiceAsync(Service service);
        Task<bool> DeleteServiceAsync(int id);
        Task<IEnumerable<ServiceCategory>> GetServiceCategoriesAsync();
    }
} 