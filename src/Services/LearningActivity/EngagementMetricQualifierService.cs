using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using Microsoft.EntityFrameworkCore;
namespace LinkedinLearningWarehouse.Services.LearningActivity
{
    public class EngagementMetricQualifierService : IEngagementMetricQualifierService
    {
        private readonly LinkedinLearningDbContext _dbContext;

        public EngagementMetricQualifierService(LinkedinLearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetMetricQualifierIdAsync(string metricQualifierName)
        {
            var metricQualifier = await _dbContext.EngagementMetricQualifiers
                .FirstOrDefaultAsync(mq => mq.MetricQualifierName == metricQualifierName);

            if (metricQualifier == null)
                throw new InvalidOperationException($"EngagementMetricQualifier '{metricQualifierName}' not found.");

            return metricQualifier.MetricQualifierId;
        }
    }
}
