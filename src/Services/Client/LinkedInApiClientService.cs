using LinkedinLearningWarehouse.Identity;
using LinkedinLearningWarehouse.Interfaces.Client;
using Newtonsoft.Json;
using Serilog;

namespace LinkedinLearningWarehouse.Services.Client
{
    public class LinkedInApiClientService<T> : ILinkedInApiClientService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public LinkedInApiClientService()
        {
            _httpClient = new HttpClient();
            _logger = Log.ForContext<LinkedInApiClientService<T>>();
        }

        public async Task<T> GetJsonResponse(OAuthTokenResponse tokenResponse, string apiUrl)
        {
            try
            {
                // Set up the HTTP request
                using (var request = new HttpRequestMessage(HttpMethod.Get, apiUrl))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

                    // Send the request and get the response
                    using (var response = await _httpClient.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();

                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        try
                        {
                            var data = JsonConvert.DeserializeObject<T>(jsonResponse);

                            return data;
                        }
                        catch (JsonException jsonEx)
                        {
                            _logger.Error($"JSON Deserialization Error: {jsonEx.Message}");
                            throw;
                        }
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.Error($"HTTP Request Error: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occurred read: {ex.Message}");
                throw;
            }
        }
    }
}
