using System.Net.Http.Headers;
using System.Text.Json;

namespace MenuProducerService.Infrastructure.Security
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpClient;

        public AuthClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("/api/auth/validate");

            return response.IsSuccessStatusCode;
        }
    }
}