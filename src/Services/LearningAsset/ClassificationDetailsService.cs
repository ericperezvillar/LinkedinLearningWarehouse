using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class ClassificationDetailsService : IClassificationDetailsService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IOwnerDetailsService _ownerDetailsService;
        private readonly IMapper _mapper;

        public ClassificationDetailsService(LinkedinLearningDbContext dbContext, IMapper mapper, IOwnerDetailsService ownerDetailsService)
        {
            _logger = Log.ForContext<AssetUrlService>();
            _dbContext = dbContext;
            _mapper = mapper;
            _ownerDetailsService = ownerDetailsService;
        }

        public async Task<int> CreateOrUpdateClassificationDetailAsync(ClassificationDto classificationDetailDto)
        {
            try
            {
                var ownerId = await _ownerDetailsService.CreateOrUpdateOwnerDetailAsync(classificationDetailDto.Owner);

                var existingClassification = await _dbContext.ClassificationDetails.FirstOrDefaultAsync(l => l.Urn == classificationDetailDto.Urn);

                if (existingClassification == null)
                {
                    var classificationDetails = _mapper.Map<ClassificationDetail>(classificationDetailDto);
                    classificationDetails.OwnerId = ownerId;
                    await _dbContext.ClassificationDetails.AddAsync(classificationDetails);
                    await _dbContext.SaveChangesAsync();
                    return classificationDetails.ClassificationId;
                }
                else
                {
                    existingClassification.OwnerId = ownerId;
                    _mapper.Map(classificationDetailDto, existingClassification);
                    _dbContext.ClassificationDetails.Update(existingClassification);
                    return existingClassification.ClassificationId;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing ClassificationDetail. Urn: {classificationDetailDto.Urn} " + ex.Message);
                throw;
            }

        }
    }
}
