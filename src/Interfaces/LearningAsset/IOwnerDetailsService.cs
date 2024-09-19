using LinkedinLearningWarehouse.DTOs;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IOwnerDetailsService
    {
        Task<int> CreateOrUpdateOwnerDetailAsync(OwnerDto ownerDto);
    }
}