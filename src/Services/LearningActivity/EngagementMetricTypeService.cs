using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Services.LearningActivity
{
    public class EngagementMetricTypeService : IEngagementMetricTypeService
    {
        private readonly LinkedinLearningDbContext _dbContext;

        public EngagementMetricTypeService(LinkedinLearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetMetricTypeIdAsync(string engagementTypeName)
        {
            var metricType = await _dbContext.EngagementMetricTypes.FirstOrDefaultAsync(mt => mt.MetricTypeName == engagementTypeName);

            if (metricType == null)
                throw new InvalidOperationException($"EngagementMetricType '{engagementTypeName}' not found.");

            return metricType.MetricTypeId;
        }
    }
}
