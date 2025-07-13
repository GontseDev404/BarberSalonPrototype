using BarberSalonPrototype.Models;

namespace BarberSalonPrototype.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffMember>> GetAllStaffAsync();
        Task<StaffMember?> GetStaffByIdAsync(int id);
        Task<IEnumerable<StaffMember>> GetActiveStaffAsync();
        Task<IEnumerable<StaffMember>> GetStaffByRoleAsync(string role);
        Task<StaffMember> CreateStaffAsync(StaffMember staffMember);
        Task<StaffMember> UpdateStaffAsync(StaffMember staffMember);
        Task<bool> DeleteStaffAsync(int id);
        Task<IEnumerable<GalleryImage>> GetStaffGalleryAsync(int staffId);
        Task<GalleryImage> AddGalleryImageAsync(GalleryImage image);
        Task<bool> RemoveGalleryImageAsync(int imageId);
    }
} 