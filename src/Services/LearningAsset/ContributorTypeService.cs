using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningActivitity;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class ContributorTypeService : IContributorTypeService
    {
        private readonly LinkedinLearningDbContext _dbContext;

        public ContributorTypeService(LinkedinLearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetContributorTypeIdAsync(string name)
        {
            var contributorType = await _dbContext.ContributorTypes
                .FirstOrDefaultAsync(at => at.Name == name);

            if (contributorType == null)
                throw new InvalidOperationException($"Contributor type {name} does not exist in the Database.");

            return contributorType.ContributorTypeId;
        }

        public async Task<List<ContributorType>> GetAllContributorTypesAsync()
        {
            return await _dbContext.ContributorTypes.ToListAsync();
        }
    }
}
