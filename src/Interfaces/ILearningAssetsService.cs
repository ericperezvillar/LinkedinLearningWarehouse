using LinkedinLearningWarehouse.Identity;

namespace LinkedinLearningWarehouse.Interfaces
{
    public interface ILearningAssetsService
    {
        Task PopulateLearningAssets(OAuthTokenResponse tokenResponse, List<string> contentUrn);

    }
}
