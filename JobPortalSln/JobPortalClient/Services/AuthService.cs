using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using JobPortalClient.Models;

namespace JobPortalClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(string login, string password)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login", new { Login = login, Password = password});

            if (!response.IsSuccessStatusCode)
                return false;

            var tokenResult = await response.Content.ReadFromJsonAsync<TokenResponse>();
            await _localStorage.SetItemAsync("authToken", tokenResult.Token);
            return true;
        }

        public async Task<bool> Register(Users user)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/register", user);
            return response.IsSuccessStatusCode;
        }


        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }

        public async Task<string> GetToken() => await _localStorage.GetItemAsync<string>("authToken");

        public async Task<bool> IsAuthenticated() => !string.IsNullOrEmpty(await GetToken());

        private class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
