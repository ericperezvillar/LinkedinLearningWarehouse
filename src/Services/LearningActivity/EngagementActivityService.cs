using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningActivity;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using LinkedinLearningWarehouse.Models.LearningActivitity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningActivity
{
    public class EngagementActivityService : IEngagementActivityService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public EngagementActivityService(LinkedinLearningDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = Log.ForContext<LinkedinLearningDbContext>();
        }

        public async Task CreateOrUpdateEngagementActivitiesAsync(List<ActivityDto> activities, int learnerId, int contentId)
        {
            try
            {
                foreach (var activityDto in activities)
                {
                    // Map the DTO to the EngagementActivity entity
                    var engagementActivity = _mapper.Map<EngagementActivity>(activityDto);

                    engagementActivity.LearnerId = learnerId;
                    engagementActivity.ContentId = contentId;

                    // Check if an existing activity matches the combination of important fields
                    var existingActivity = await _dbContext.EngagementActivities
                        .FirstOrDefaultAsync(ea => ea.ContentId == engagementActivity.ContentId &&
                                                   ea.LearnerId == engagementActivity.LearnerId &&
                                                   ea.AssetTypeId == (engagementActivity.AssetTypeId == null ? 0 : engagementActivity.AssetTypeId) &&
                                                   ea.MetricTypeId == engagementActivity.MetricTypeId &&
                                                   ea.MetricQualifierId == engagementActivity.MetricQualifierId);

                    if (existingActivity != null)
                    {
                        existingActivity.EngagementValue = engagementActivity.EngagementValue;
                        existingActivity.FirstEngagedAt = engagementActivity.FirstEngagedAt;
                        existingActivity.LastEngagedAt = engagementActivity.LastEngagedAt;
                        _dbContext.EngagementActivities.Update(existingActivity);
                    }
                    else
                    {
                        await _dbContext.EngagementActivities.AddAsync(engagementActivity);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing list of EngagementActivities for LearnerId {learnerId} and ContentId {contentId}. {ex.Message}");
                throw;
            }
        }
    }
}
