using Microsoft.Extensions.DependencyInjection;
using Serilog;
using LinkedinLearningWarehouse.Interfaces;
using LinkedinLearningWarehouse.Interfaces.Client;
using Microsoft.Extensions.Options;

namespace LinkedinLearningWarehouse
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Setup DI and configure services
            var serviceProvider = Startup.ConfigureServices();

            // Retrieve the LinkedIn token service
            var linkedInAuthService = serviceProvider.GetRequiredService<ILinkedInAuthService>();

            Log.Information("Retreving Access Token...");

            // Get an OAuth token
            var token = await linkedInAuthService.GetAccessTokenAsync();

            if (token == null)
            {
                Log.Error("Failed to retrieve OAuth Token."); 
                Environment.Exit(1);
            }

            var apiClientLearningActivity = serviceProvider.GetRequiredService<ILearningActivityService>();
            var apiClientLearningAssets = serviceProvider.GetRequiredService<ILearningAssetsService>();
            var integratorHistoryService = serviceProvider.GetRequiredService<IIntegratorHistoryService>();
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;

            var dates = new List<DateTime>();

            var latestRecordedDate = await integratorHistoryService.GetLatestProcessDateAsync();

            for (DateTime date = latestRecordedDate.AddDays(1); date <= DateTime.Today; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            foreach (DateTime dateToProcess in dates)
            {
                Log.Information($"Date {dateToProcess}. Populate Learning Activity Report details.");
                var contentUrn = await apiClientLearningActivity.PopulateLearningActivity(token, dateToProcess);
                Log.Information($"Date {dateToProcess}. Populate Learning Assets Report details.");
                await apiClientLearningAssets.PopulateLearningAssets(token, contentUrn);
                await integratorHistoryService.SetNewHistoryProcessAsync(dateToProcess);

                Thread.Sleep(appSettings.TimeBetweenCallsInMill);
            }
        }
    }
}