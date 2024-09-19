using AutoMapper;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningActivitity;

namespace LinkedinLearningWarehouse.Mappers.Resolvers
{
    public class ContributorTypeResolver : IValueResolver<ContributorDetailDto, ContributorDetail, int>
    {
        private readonly IContributorTypeService _contributorTypeService;

        public ContributorTypeResolver(IContributorTypeService contributorTypeService)
        {
            _contributorTypeService = contributorTypeService;
        }

        public int Resolve(ContributorDetailDto source, ContributorDetail destination, int destMember, ResolutionContext context)
        {
            return _contributorTypeService.GetContributorTypeIdAsync(source.ContributionType).Result;
        }
    }
}
