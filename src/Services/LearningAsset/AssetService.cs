using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class AssetService : IAssetService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AssetService(LinkedinLearningDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = Log.ForContext<AssetService>();
        }

        public async Task<int> CreateAsset(AssetDto assetDto)
        {
            try
            {               
                var newAsset = _mapper.Map<Asset>(assetDto);
                await _dbContext.Assets.AddAsync(newAsset);   
                await _dbContext.SaveChangesAsync();
                return newAsset.AssetId;

            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing CreateAsset. Asset Urn: {assetDto.Urn} " + ex.Message);
                throw;
            }
        }

        public async Task<int> CreateAssetFromRoot(RootObjectLearningAsset assetDto)
        {
            try
            {
                var newAsset = _mapper.Map<Asset>(assetDto);
                await _dbContext.Assets.AddAsync(newAsset);
                await _dbContext.SaveChangesAsync();
                return newAsset.AssetId;

            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing CreateAsset. Asset Urn: {assetDto.Urn} " + ex.Message);
                throw;
            }
        }

        public async Task<Asset> GetAsset(string urn)
        {
            try
            {
                return await _dbContext.Assets.AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync(ac => ac.Urn == urn);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing GetAsset. Asset Urn: {urn} " + ex.Message);
                throw;
            }
        }
    }
}
