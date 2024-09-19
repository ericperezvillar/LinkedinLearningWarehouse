using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningActivity;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using LinkedinLearningWarehouse.Models.LearningActivitity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class ContentDetailsService : IContentDetailsService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ContentDetailsService(LinkedinLearningDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = Log.ForContext<AssetUrlService>();
        }

        public async Task<int> CreateOrUpdateContentDetailsAsync(ContentDetailsDto contentDetailsDto)
        {
            try
            {
                // Find existing content by ContentUrn, assuming it's unique
                var existingContentDetail = await _dbContext.ContentDetails.FirstOrDefaultAsync(cd => cd.ContentUrn == contentDetailsDto.ContentUrn);

                if (existingContentDetail != null)
                {
                    _mapper.Map(contentDetailsDto, existingContentDetail);
                    _dbContext.ContentDetails.Update(existingContentDetail);
                    return existingContentDetail.ContentId;
                }
                else
                {
                    var newContentDetail = _mapper.Map<ContentDetail>(contentDetailsDto);
                    await _dbContext.ContentDetails.AddAsync(newContentDetail);
                    await _dbContext.SaveChangesAsync();
                    return newContentDetail.ContentId;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing ContentDetails. Urn: {contentDetailsDto.ContentUrn} " + ex.Message);
                throw;
            }
            
        }
    }
}
