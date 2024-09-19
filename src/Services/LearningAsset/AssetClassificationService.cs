using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class AssetClassificationService : IAssetClassificationService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly IClassificationDetailsService _classificationDetailsService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AssetClassificationService(LinkedinLearningDbContext dbContext, IMapper mapper, IClassificationDetailsService classificationDetailsService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = Log.ForContext<AssetClassificationService>();
            _classificationDetailsService = classificationDetailsService;
        }

        public async Task CreateOrUpdateAssetClassificationAsync(AssetClassificationDto assetClassificationDto, int assetId)
        {
            try
            {
                // Associated classification should always exists
                var associatedClassificationId = await _classificationDetailsService.CreateOrUpdateClassificationDetailAsync(assetClassificationDto.AssociatedClassificationDto);

                var existingAssetClassification = await _dbContext.AssetClassifications
                    .FirstOrDefaultAsync(ac => ac.AssetId == assetId && ac.ClassificationId == associatedClassificationId);

                if (existingAssetClassification != null)
                {
                    existingAssetClassification.AssignerName = assetClassificationDto.Assigner.Name.Value;
                    existingAssetClassification.AssignerUrn = assetClassificationDto.Assigner.Urn;
                    _dbContext.AssetClassifications.Update(existingAssetClassification);
                    if (assetClassificationDto.PathDto != null && assetClassificationDto.PathDto.Any())
                        await HandleAssetClassificationPathsAsync(assetClassificationDto.PathDto, existingAssetClassification.AssetClassificationId);
                }
                else
                {
                    // Map the DTO to the AssetClassification entity
                    var assetClassification = _mapper.Map<AssetClassification>(assetClassificationDto);
                    assetClassification.ClassificationId = associatedClassificationId;
                    assetClassification.AssetId = assetId;
                    await _dbContext.AssetClassifications.AddAsync(assetClassification);
                    await _dbContext.SaveChangesAsync();
                    if (assetClassificationDto.PathDto != null && assetClassificationDto.PathDto.Any())
                        await HandleAssetClassificationPathsAsync(assetClassificationDto.PathDto, assetClassification.AssetClassificationId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing AssetClassification. AssetId: {assetId} - {ex.Message}");
                throw;
            }
        }

        public async Task HandleAssetClassificationPathsAsync(List<ClassificationDto> pathDtos, int assetClassificationId)
        {
            try
            {
                // Remove existing paths for this AssetClassification
                var existingPaths = await _dbContext.AssetClassificationPaths
                    .Where(p => p.AssetClassificationId == assetClassificationId)
                    .ToListAsync();

                _dbContext.AssetClassificationPaths.RemoveRange(existingPaths);
                await _dbContext.SaveChangesAsync();

                // Add new paths
                foreach (var pathDto in pathDtos)
                {
                    // Ensure ClassificationDetail exists
                    var classificationId = await _classificationDetailsService.CreateOrUpdateClassificationDetailAsync(pathDto);

                    var pathDetail = new AssetClassificationPath
                    {
                        AssetClassificationId = assetClassificationId,
                        ClassificationId = classificationId
                    };

                    await _dbContext.AssetClassificationPaths.AddAsync(pathDetail);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing AssetClassificationPath. AssetClassificationId: {assetClassificationId}, Error: {ex.Message}");
                throw;
            }
        }

    }
}
