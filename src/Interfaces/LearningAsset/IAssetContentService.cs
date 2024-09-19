using LinkedinLearningWarehouse.DTOs.LearningAsset;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IAssetContentService
    {
        Task CreateAssetContent(AssetContentDto assetContentDto, int assetId);
    }
}