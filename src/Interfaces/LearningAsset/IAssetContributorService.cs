using LinkedinLearningWarehouse.DTOs.LearningAsset;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IAssetContributorService
    {
        Task CreateOrUpdateAssetContributor(ContributorDetailDto contributorDetailDto, int assetId);
    }
}