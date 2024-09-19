using AutoMapper;
using LinkedinLearningWarehouse.DTOs.LearningActivity;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using LinkedinLearningWarehouse.Models.LearningActivitity;

namespace LinkedinLearningWarehouse.Mappers.Resolvers
{
    public class EngagementMetricQualifierResolver : IValueResolver<ActivityDto, EngagementActivity, int>
    {
        private readonly IEngagementMetricQualifierService _engagementMetricQualifierService;

        public EngagementMetricQualifierResolver(IEngagementMetricQualifierService engagementMetricQualifierService)
        {
            _engagementMetricQualifierService = engagementMetricQualifierService;
        }

        public int Resolve(ActivityDto source, EngagementActivity destination, int destMember, ResolutionContext context)
        {
            return _engagementMetricQualifierService.GetMetricQualifierIdAsync(source.EngagementMetricQualifier).Result;
        }
    }
}
