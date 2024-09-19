using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class AssetUrlService : IAssetUrlService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AssetUrlService(LinkedinLearningDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = Log.ForContext<AssetUrlService>();
        }

        public async Task CreateOrUpdateAssetUrl(AssetUrlsDto assetUrlDto, int assetId)
        {
            try
            {
                var existingAssetUrl = _dbContext.AssetUrls
                        .FirstOrDefault(x => x.AssetId == assetId);

                if (existingAssetUrl != null)
                {
                    _dbContext.AssetUrls.Update(existingAssetUrl);
                }
                else
                {
                    var newAssetUrl = _mapper.Map<AssetUrl>(assetUrlDto);
                    newAssetUrl.AssetId = assetId;
                    await _dbContext.AssetUrls.AddAsync(newAssetUrl);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing AssetUrl. AssetId: {assetId} " + ex.Message);
                throw;
            }
        }
    }
}
