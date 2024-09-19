using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Interfaces.LearningAsset
{
    public interface IClassificationTypeService
    {
        Task<int?> GetClassificationTypeIdAsync(string classificationTypeName);

        Task<List<ClassificationType>> GetAllClassificationTypesAsync();
    }
}
