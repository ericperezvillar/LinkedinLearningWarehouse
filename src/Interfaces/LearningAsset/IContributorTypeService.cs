using LinkedinLearningWarehouse.Models.LearningActivitity;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IContributorTypeService
    {
        Task<int> GetContributorTypeIdAsync(string name);

        Task<List<ContributorType>> GetAllContributorTypesAsync();
    }
}
