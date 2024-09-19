using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningActivitity;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class ContributorDetailService : IContributorDetailService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ContributorDetailService(LinkedinLearningDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = Log.ForContext<ContributorDetailService>();
        }

        public async Task<int> CreateOrUpdateContributorDetail(ContributorDetailDto contributorDetailDto)
        {
            try
            {
                var existingContributorDetail = _dbContext.ContributorDetails
                    .FirstOrDefault(x => x.Urn == contributorDetailDto.Urn);

                if (existingContributorDetail == null)
                {
                    var newContributorDetail = _mapper.Map<ContributorDetail>(contributorDetailDto);
                    await _dbContext.ContributorDetails.AddAsync(newContributorDetail);
                    await _dbContext.SaveChangesAsync();
                    return newContributorDetail.ContributorId;
                }
                else
                {
                    _dbContext.ContributorDetails.Update(existingContributorDetail);
                    return existingContributorDetail.ContributorId;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing ContributorDetail. Urn: {contributorDetailDto.Urn} " + ex.Message);
                throw;
            }
        }
    }
}
