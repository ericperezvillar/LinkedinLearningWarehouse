using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class AssetContentService : IAssetContentService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IAssetService _assetService;
        private readonly IMapper _mapper;

        public AssetContentService(LinkedinLearningDbContext dbContext, IAssetService assetService, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _assetService = assetService;
            _logger = Log.ForContext<AssetContentService>();
        }

        public async Task CreateAssetContent(AssetContentDto assetContentDto, int assetId)
        {
            try
            {
                // 2. Recursively add new AssetContents from AssetContentDto
                var newAssetContents = new List<AssetContent>();
                await AddAssetContentsRecursively(assetContentDto, assetId, newAssetContents);

                // 3. Add all the new AssetContents to the DbContext
                await _dbContext.AssetContents.AddRangeAsync(newAssetContents);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing CreateAssetContent. AssetId: {assetId} " + ex.Message);
                throw;
            }
        }

        // <summary>
        /// Recursively adds AssetContents from AssetContentDto to the list of AssetContent entities.
        /// </summary>
        /// <param name="assetContentDto">The DTO containing asset content information.</param>
        /// <param name="parentAssetId">The parent asset ID to associate with the content.</param>
        /// <param name="newAssetContents">The list of AssetContent entities being constructed.</param>
        private async Task AddAssetContentsRecursively(AssetContentDto assetContentDto, int parentAssetId, List<AssetContent> newAssetContents)
        {
            var assetContent = _mapper.Map<AssetContent>(assetContentDto);
            assetContent.ParentAssetId = parentAssetId;

            var existingAsset = await _assetService.GetAsset(assetContentDto.Asset.Urn);

            if (existingAsset != null)
                assetContent.ChildAssetId = existingAsset.AssetId;
            else
            { 
                var assetCreatedId = await _assetService.CreateAsset(assetContentDto.Asset);
                assetContent.ChildAssetId = assetCreatedId;
            }

            bool assetContentExists = await _dbContext.AssetContents.AnyAsync(ac => ac.ParentAssetId == assetContent.ParentAssetId
                                                               && ac.ChildAssetId == assetContent.ChildAssetId);
            if (!assetContentExists)
            {
                newAssetContents.Add(assetContent);
            }

            // Recursively process any nested contents
            foreach (var nestedContentDto in assetContentDto.Asset.Contents)
            {
                await AddAssetContentsRecursively(nestedContentDto, assetContent.ChildAssetId, newAssetContents);
            }
        }
    }
}
