using LinkedinLearningWarehouse.DTOs.LearningAsset;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IAssetClassificationService
    {
        Task CreateOrUpdateAssetClassificationAsync(AssetClassificationDto assetClassificationDto, int assetId);
    }
}