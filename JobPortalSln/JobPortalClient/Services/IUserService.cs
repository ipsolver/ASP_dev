using JobPortalClient.Models;
using JobPortalClient.Services;

namespace JobPortalClient.Services
{
    public interface IUserService
    {
        Task<List<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(long id);
        Task CreateAsync(Users user);
        Task UpdateAsync(long id, Users user);
        Task DeleteAsync(long id);
    }

}
