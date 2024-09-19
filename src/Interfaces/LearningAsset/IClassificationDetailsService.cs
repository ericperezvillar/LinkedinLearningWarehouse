using LinkedinLearningWarehouse.DTOs.LearningAsset;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IClassificationDetailsService
    {
        Task<int> CreateOrUpdateClassificationDetailAsync(ClassificationDto classificationDetailDto);
    }
}