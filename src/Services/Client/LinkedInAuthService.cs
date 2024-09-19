using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using LinkedinLearningWarehouse.Identity;
using Microsoft.Extensions.Options;
using LinkedinLearningWarehouse.Interfaces.Client;
using Serilog;

namespace LinkedinLearningWarehouse.Services.Client
{
    public class LinkedInAuthService : ILinkedInAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;

        public LinkedInAuthService(IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _logger = Log.ForContext<LinkedInAuthService>();
            _appSettings = appSettings.Value;
        }

        public async Task<OAuthTokenResponse> GetAccessTokenAsync()
        {
            var accessTokenUrl = _appSettings.TokenUrl;
            var clientId = _configuration["ClientId"];
            var clientSecret = _configuration["ClientSecret"];
            var grantType = _appSettings.TokenGrantType;

            var requestContent = new StringContent(
                $"grant_type={grantType}&client_id={clientId}&client_secret={clientSecret}",
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, accessTokenUrl)
            {
                Content = requestContent
            };

            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var response = await _httpClient.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<OAuthTokenResponse>(jsonResponse);
                    return tokenResponse;
                }
                else
                {
                    // Log response status and content for debugging
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.Information($"Response Status: {response.StatusCode}");
                    _logger.Information($"Response Content: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception trying to get Token from {accessTokenUrl}. {ex.Message}.");
            }            

            return null;
        }
    }
}
