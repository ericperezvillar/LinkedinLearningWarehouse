using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class ClassificationTypeService : IClassificationTypeService
    {
        private readonly LinkedinLearningDbContext _dbContext;

        public ClassificationTypeService(LinkedinLearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int?> GetClassificationTypeIdAsync(string classificationTypeName)
        {
            var classificationType = await _dbContext.ClassificationTypes
                .FirstOrDefaultAsync(at => at.Name == classificationTypeName);

            if (classificationType == null)
                return null;

            return classificationType.ClassificationTypeId;
        }

        public async Task<List<ClassificationType>> GetAllClassificationTypesAsync()
        {
            return await _dbContext.ClassificationTypes.ToListAsync();
        }
    }
}
