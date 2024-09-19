using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Models;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IContributorDetailService
    {
        Task<int> CreateOrUpdateContributorDetail(ContributorDetailDto contributorDetailDto);
    }
}