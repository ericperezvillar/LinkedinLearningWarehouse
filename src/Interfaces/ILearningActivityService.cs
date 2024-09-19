using LinkedinLearningWarehouse.Identity;

namespace LinkedinLearningWarehouse.Interfaces
{
    public interface ILearningActivityService
    {
        Task<List<string>> PopulateLearningActivity(OAuthTokenResponse tokenResponse, DateTime dateToProcess);
    }
}
