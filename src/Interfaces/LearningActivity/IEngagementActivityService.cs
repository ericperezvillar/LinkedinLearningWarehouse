using LinkedinLearningWarehouse.DTOs.LearningActivity;

namespace LinkedinLearningWarehouse.Interfaces.LearningActivity
{
    public interface IEngagementActivityService
    {
        Task CreateOrUpdateEngagementActivitiesAsync(List<ActivityDto> activities, int learnerId, int contentId);

    }
}
