using LinkedinLearningWarehouse.DTOs;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IAssetAvailableLocaleService
    {
        Task CreateAssetAvailableLocale(List<LocaleDto> localeDtos, int assetId);
    }
}