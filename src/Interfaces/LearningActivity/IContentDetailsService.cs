using LinkedinLearningWarehouse.DTOs.LearningActivity;

namespace LinkedinLearningWarehouse.Interfaces.LearningActivity
{
    public interface IContentDetailsService
    {
        Task<int> CreateOrUpdateContentDetailsAsync(ContentDetailsDto contentDetailsDto);
    }
}
