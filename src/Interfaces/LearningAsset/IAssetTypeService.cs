using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IAssetTypeService
    {
        Task<int> GetAssetTypeIdAsync(string assetTypeName);

        Task<List<AssetType>> GetAllActiveAssetTypesAsync();
    }
}
