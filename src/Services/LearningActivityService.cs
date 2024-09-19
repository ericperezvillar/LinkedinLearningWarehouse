using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningActivity;
using LinkedinLearningWarehouse.Identity;
using LinkedinLearningWarehouse.Interfaces;
using LinkedinLearningWarehouse.Interfaces.Client;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using Microsoft.Extensions.Options;
using Serilog;

namespace LinkedinLearningWarehouse.Services
{
    public class LearningActivityService : ILearningActivityService
    {
        private readonly ILinkedInApiClientService<RootObjectLearningActivity> _linkedInApiClientService;
        private readonly ILearnerDetailsService _learnerDetailsService;
        private readonly IEnterpriseGroupsService _enterpriseGroupsService;
        private readonly IEngagementActivityService _engagementActivityService;
        private readonly IContentDetailsService _contentDetailsService;
        private readonly IQueryParameterBuilderService _parameterBuilderService;
        private readonly AppSettings _appSettings;
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;

        public LearningActivityService(ILinkedInApiClientService<RootObjectLearningActivity> linkedInApiClientService, 
                                    LinkedinLearningDbContext dbContext, 
                                    ILearnerDetailsService learnerDetailsService, 
                                    IEnterpriseGroupsService enterpriseGroupsService, 
                                    IEngagementActivityService engagementActivityService, 
                                    IContentDetailsService contentDetailsService, 
                                    IQueryParameterBuilderService parameterBuilderService, 
                                    IOptions<AppSettings> appSettings)
        {
            _logger = Log.ForContext<LearningActivityService>();
            _linkedInApiClientService = linkedInApiClientService;
            _dbContext = dbContext;
            _learnerDetailsService = learnerDetailsService;
            _enterpriseGroupsService = enterpriseGroupsService;
            _engagementActivityService = engagementActivityService;
            _contentDetailsService = contentDetailsService;
            _parameterBuilderService = parameterBuilderService;
            _appSettings = appSettings.Value;
        }

        public async Task<List<string>> PopulateLearningActivity(OAuthTokenResponse tokenResponse, DateTime dateToProcess)
        {
            var listOfQueries = await _parameterBuilderService.BuildQueryString(dateToProcess);

            var listOfContentUrn = new List<string>();

            foreach (var query in listOfQueries)
            {
                try
                {
                    var requestUrl = $"{_appSettings.LearningActivityUrl}?{query}";

                    var result = await _linkedInApiClientService.GetJsonResponse(tokenResponse, requestUrl);

                    var contentsUrns = await SaveDataAsync(result);

                    listOfContentUrn.AddRange(contentsUrns);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error processing Learning Activity - Date {dateToProcess} - Query {query} - Error: { ex.Message }");
                }
            }

            return listOfContentUrn;

        }

        private async Task<List<string>> SaveDataAsync(RootObjectLearningActivity data)
        {
            try
            {
                var listOfContentUrn = new List<string>();
                foreach (var element in data.Elements)
                {

                    var learnerId = await _learnerDetailsService.CreateOrUpdateLearnerDetailAsync(element.LearnerDetailsDto);

                    await _enterpriseGroupsService.CreateOrUpdateEnterpriseGroupAsync(element.LearnerDetailsDto.EnterpriseGroups, learnerId);

                    var contentId = await _contentDetailsService.CreateOrUpdateContentDetailsAsync(element.ContentDetailsDto);

                    await _engagementActivityService.CreateOrUpdateEngagementActivitiesAsync(element.ActivitiesDtos, learnerId, contentId);

                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();

                    listOfContentUrn.Add(element.ContentDetailsDto.ContentUrn);                
                }

                return listOfContentUrn;
            }
            catch (Exception ex)
            {
                _logger.Error("Error processing list of element from JSON for PopulateLearningActivity. " + ex.Message);
                throw;
            }                       
        }
    }
}
