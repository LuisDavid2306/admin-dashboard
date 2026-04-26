using AdminDashboard.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AdminDashboard.Services

{
    public class DashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardViewModel> ObtenerDashboardAsync(string token, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

                var url = "https://app-bancaria-backend.onrender.com/api/Admin/dashboard";

                if (fechaInicio.HasValue && fechaFin.HasValue)
                {
                    url += $"?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}";
                }

                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return null;

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<ApiResponse<DashboardViewModel>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result?.Data;
            }
            catch
            {
                return null;
            }
        }


    }
}
