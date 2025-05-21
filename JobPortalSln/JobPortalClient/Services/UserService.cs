using System.Net.Http.Json;
using JobPortalClient.Services;
using JobPortalClient.Models;

namespace JobPortalClient.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Users>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<Users>>("api/Users");

        public async Task<Users?> GetByIdAsync(long id) =>
            await _http.GetFromJsonAsync<Users>($"api/Users/{id}");

        public async Task CreateAsync(Users user)
        {
            var response = await _http.PostAsJsonAsync("api/Users", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(long id, Users user)
        {
            var response = await _http.PutAsJsonAsync($"api/Users/{id}", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(long id)
        {
            var response = await _http.DeleteAsync($"api/Users/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}
