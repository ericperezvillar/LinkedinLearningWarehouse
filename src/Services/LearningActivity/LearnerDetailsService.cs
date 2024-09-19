using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningActivity;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using LinkedinLearningWarehouse.Models.LearningActivitity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningActivity
{
    public class LearnerDetailsService : ILearnerDetailsService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LearnerDetailsService(LinkedinLearningDbContext dbContext, IMapper mapper)
        {
            _logger = Log.ForContext<LearnerDetailsService>();
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateOrUpdateLearnerDetailAsync(LearnerDetailsDto learnerDto)
        {
            try
            {
                var existingLearner = await _dbContext.LearnerDetails.FirstOrDefaultAsync(l => l.Email == learnerDto.Email);

                if (existingLearner == null)
                {
                    var learner = _mapper.Map<LearnerDetail>(learnerDto);
                    _dbContext.LearnerDetails.Add(learner);
                    await _dbContext.SaveChangesAsync();
                    return learner.LearnerId;
                }
                else
                {
                    _mapper.Map(learnerDto, existingLearner);
                    _dbContext.LearnerDetails.Update(existingLearner);
                    return existingLearner.LearnerId;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing list of LearnerDetail for Learner email {learnerDto.Email}. {ex.Message}");
                throw;
            }
            
        }
    }
}
