using LinkedinLearningWarehouse.DTOs.LearningAsset;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IAssetUrlService
    {
        Task CreateOrUpdateAssetUrl(AssetUrlsDto assetUrlDto, int assetId);
    }
}
