using AdminDashboard.Models;
using AdminDashboard.Models.DTO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AdminDashboard.Services
{
    public class AdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsuarioAdminDto>> GetUsuarios(string token, string? search)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = "https://app-bancaria-backend.onrender.com/api/Admin/usuarios";

                if (!string.IsNullOrEmpty(search))
                    url += $"?search={search}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new List<UsuarioAdminDto>();

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<ApiResponse<List<UsuarioAdminDto>>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result?.Data ?? new List<UsuarioAdminDto>();
            }
            catch
            {
                return new List<UsuarioAdminDto>();
            }
        }
        public async Task<ApiResponse<string>> CrearUsuario(string token, CrearUsuarioDto dto)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://app-bancaria-backend.onrender.com/api/Admin/usuarios", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<string>>(jsonResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<ApiResponse<string>> EditarUsuario(string token, EditarUsuarioDto dto)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("https://app-bancaria-backend.onrender.com/api/Admin/usuarios", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<string>>(jsonResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
        public async Task<ApiResponse<string>> EliminarUsuario(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"https://app-bancaria-backend.onrender.com/api/Admin/usuarios/{id}");

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<string>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<List<TransaccionAdminDto>> GetTransacciones(string token, DateTime? inicio, DateTime? fin, string? tipo, string? usuario)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var url = "https://app-bancaria-backend.onrender.com/api/Admin/transacciones";

            var queryParams = new List<string>();

            if (inicio.HasValue) queryParams.Add($"fechaInicio={inicio:yyyy-MM-dd}");
            if (fin.HasValue) queryParams.Add($"fechaFin={fin:yyyy-MM-dd}");
            if (!string.IsNullOrEmpty(tipo)) queryParams.Add($"tipo={tipo}");
            if (!string.IsNullOrEmpty(usuario)) queryParams.Add($"usuario={usuario}");

            if (queryParams.Any())
                url += "?" + string.Join("&", queryParams);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<TransaccionAdminDto>();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<List<TransaccionAdminDto>>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.Data ?? new List<TransaccionAdminDto>();
        }
        public async Task<List<GrupoAdminDto>> GetGrupos(string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("https://app-bancaria-backend.onrender.com/api/Admin/grupos");

            if (!response.IsSuccessStatusCode)
                return new List<GrupoAdminDto>();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<List<GrupoAdminDto>>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.Data ?? new List<GrupoAdminDto>();
        }
        public async Task<GrupoDetalleDto> GetGrupoDetalle(string token, string codgrupo)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"https://app-bancaria-backend.onrender.com/api/Admin/grupos/{codgrupo}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<GrupoDetalleDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.Data;
        }
    }
}
