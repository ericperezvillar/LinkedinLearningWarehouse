using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Interfaces;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models;
using LinkedinLearningWarehouse.Utility;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text;

namespace LinkedinLearningWarehouse.Services
{
    public class QueryParameterBuilderService : IQueryParameterBuilderService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IAssetTypeService _assetTypeService;

        public QueryParameterBuilderService(LinkedinLearningDbContext dbContext, IAssetTypeService assetTypeService)
        {
            _logger = Log.ForContext<QueryParameterBuilderService>();
            _dbContext = dbContext;
            _assetTypeService = assetTypeService;
        }

        public async Task<List<string>> BuildQueryString(DateTime dateProcessor)
        {
            var queryStrings = new List<string>();

            try
            {
                var dateToProcess = TransformJsonDataHelper.DateTimeToUnixTimeStamp(dateProcessor);

                var activeParameters = await GetQueryParameterAsync();
                var assetTypes = await _assetTypeService.GetAllActiveAssetTypesAsync();

                foreach (var assetType in assetTypes)
                {
                    var queryString = new StringBuilder();

                    foreach (var param in activeParameters)
                    {
                        queryString.Append($"{param.Name}={param.Value}&");
                    }

                    // Add the specific assetType
                    queryString.Append($"assetType={assetType.AssetTypeName}&");

                    // Append the startedAt parameter
                    queryString.Append($"startedAt={dateToProcess}");
                    // Add the constructed query string to the list
                    queryStrings.Add(queryString.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error processing list of element from JSON. " + ex.Message);
            }
            return queryStrings;
        }

        private async Task<List<QueryParameter>> GetQueryParameterAsync()
        {
            return await _dbContext.QueryParameters.Where(x => x.IsActive).ToListAsync();
        }
    }
}
