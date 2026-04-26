using AdminDashboard.Models;
using System.Text;
using System.Text.Json;

namespace AdminDashboard.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> LoginAsync(LoginViewModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://app-bancaria-backend.onrender.com/api/Auth/login", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var resultJson = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<LoginResponse>>(resultJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.Data?.Token;
        }

        public class LoginResponse
        {
            public string Token { get; set; } = "";
        }

    }
}
