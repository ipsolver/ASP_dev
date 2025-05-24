using JobPortalClient.Models;

namespace JobPortalClient.Services
{
    public interface IAuthService
    {
        Task<bool> Login(string login, string password);
        Task<bool> Register(Users user);

        Task Logout();
        Task<string> GetToken();
        Task<bool> IsAuthenticated();
    }
}
