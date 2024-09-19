using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class AssetTypeService : IAssetTypeService
    {
        private readonly LinkedinLearningDbContext _dbContext;

        public AssetTypeService(LinkedinLearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetAssetTypeIdAsync(string assetTypeName)
        {
            var assetType = await _dbContext.AssetTypes
                .FirstOrDefaultAsync(at => at.AssetTypeName == assetTypeName);

            if (assetType == null)
                throw new InvalidOperationException($"Asset type {assetTypeName} does not exist in the Database.");

            return assetType.AssetTypeId;
        }

        public async Task<List<AssetType>> GetAllActiveAssetTypesAsync()
        {
            return await _dbContext.AssetTypes.Where(x => x.Active).ToListAsync();
        }
    }
}
