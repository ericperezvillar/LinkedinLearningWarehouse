using AutoMapper;
using LinkedinLearningWarehouse.DTOs.LearningActivity;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using LinkedinLearningWarehouse.Models.LearningActivitity;

namespace LinkedinLearningWarehouse.Mappers.Resolvers
{
    public class EngagementMetricTypeResolver : IValueResolver<ActivityDto, EngagementActivity, int>
    {
        private readonly IEngagementMetricTypeService _engagementMetricTypeService;

        public EngagementMetricTypeResolver(IEngagementMetricTypeService engagementMetricTypeService)
        {
            _engagementMetricTypeService = engagementMetricTypeService;
        }

        public int Resolve(ActivityDto source, EngagementActivity destination, int destMember, ResolutionContext context)
        {
            return _engagementMetricTypeService.GetMetricTypeIdAsync(source.EngagementType).Result;
        }
    }
}
