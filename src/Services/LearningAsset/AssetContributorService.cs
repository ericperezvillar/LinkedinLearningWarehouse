using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class AssetContributorService : IAssetContributorService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly IContributorDetailService _contributorDetailService;
        private readonly ILogger _logger;

        public AssetContributorService(LinkedinLearningDbContext dbContext, IContributorDetailService contributorDetailService)
        {
            _dbContext = dbContext;
            _logger = Log.ForContext<AssetContentService>();
            _contributorDetailService = contributorDetailService;
        }

        public async Task CreateOrUpdateAssetContributor(ContributorDetailDto contributorDetailDto, int assetId)
        {
            try
            {
                var contributorId = await _contributorDetailService.CreateOrUpdateContributorDetail(contributorDetailDto);

                var existingAssetContributor = await _dbContext.AssetContributors
                    .FirstOrDefaultAsync(ac => ac.AssetId == assetId && ac.ContributorId == contributorId);

                if (existingAssetContributor == null)
                {
                    var newAssetContributor = new AssetContributor() { AssetId = assetId, ContributorId = contributorId };
                    await _dbContext.AssetContributors.AddAsync(newAssetContributor);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing AssetContributor. AssetId: {assetId} " + ex.Message);
                throw;
            }
        }
    }
}
