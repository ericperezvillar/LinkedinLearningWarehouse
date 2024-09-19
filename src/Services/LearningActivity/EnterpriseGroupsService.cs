using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using LinkedinLearningWarehouse.Models.LearningActivitity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningActivity
{
    public class EnterpriseGroupsService : IEnterpriseGroupsService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;

        public EnterpriseGroupsService(LinkedinLearningDbContext dbContext)
        {
            _logger = Log.ForContext<EnterpriseGroupsService>();
            _dbContext = dbContext;
        }

        public async Task CreateOrUpdateEnterpriseGroupAsync(List<string> enterpriseGroups, int learnerId)
        {
            try
            {
                var existingEnterpriseGroups = await _dbContext.EnterpriseGroups
                .Where(eg => eg.LearnerId == learnerId)
                    .ToListAsync();

                    var newEnterpriseGroups = enterpriseGroups
                        .Except(existingEnterpriseGroups.Select(eg => eg.GroupName))
                        .Select(g => new EnterpriseGroup
                        {
                            GroupName = g,
                            LearnerId = learnerId
                        }).ToList();

                    if (newEnterpriseGroups.Any())
                    {
                        _dbContext.EnterpriseGroups.AddRange(newEnterpriseGroups);
                    }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing list of EnterpriseGroup for LearnerId {learnerId}. {ex.Message}");
                throw;
            }            
        }
    }
}
