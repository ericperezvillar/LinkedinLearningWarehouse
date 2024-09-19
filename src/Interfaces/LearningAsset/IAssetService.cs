using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IAssetService
    {
        Task<int> CreateAsset(AssetDto assetDto);

        Task<int> CreateAssetFromRoot(RootObjectLearningAsset assetDto);

        Task<Asset> GetAsset(string urn);
    }
}