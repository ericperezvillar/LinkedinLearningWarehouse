using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class OwnerDetailsService : IOwnerDetailsService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OwnerDetailsService(LinkedinLearningDbContext dbContext, IMapper mapper)
        {
            _logger = Log.ForContext<OwnerDetailsService>();
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateOrUpdateOwnerDetailAsync(OwnerDto ownerDto)
        {
            try
            {
                var existingOwner = await _dbContext.OwnerDetails.FirstOrDefaultAsync(l => l.Urn == ownerDto.Urn);

                if (existingOwner == null)
                {
                    var owner = _mapper.Map<OwnerDetail>(ownerDto);
                    _dbContext.OwnerDetails.Add(owner);
                    await _dbContext.SaveChangesAsync();
                    return owner.OwnerId;
                }
                else
                {
                    _mapper.Map(ownerDto, existingOwner);
                    _dbContext.OwnerDetails.Update(existingOwner);
                    return existingOwner.OwnerId;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing OwnerDetail. Urn: {ownerDto.Urn} " + ex.Message);
                throw;
            }
        }
    }
}
