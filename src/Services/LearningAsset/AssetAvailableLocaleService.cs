using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using LinkedinLearningWarehouse.Services.LearningActivity;
using Serilog;

namespace LinkedinLearningWarehouse.Services.LearningAsset
{
    public class AssetAvailableLocaleService : IAssetAvailableLocaleService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AssetAvailableLocaleService(LinkedinLearningDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = Log.ForContext<LearnerDetailsService>();
        }

        public async Task CreateAssetAvailableLocale(List<LocaleDto> localeDtos, int assetId)
        {
            try
            {
                foreach (var localeDto in localeDtos)
                {
                    var existingLocale = _dbContext.AssetAvailableLocales
                        .FirstOrDefault(x => x.AssetId == assetId
                                            && x.Language == localeDto.Language
                                            && x.Country == localeDto.Country);
                    if (existingLocale == null)
                    {
                        var newAssetAvailableLocale = _mapper.Map<AssetAvailableLocale>(localeDto);
                        newAssetAvailableLocale.AssetId = assetId;
                        await _dbContext.AssetAvailableLocales.AddAsync(newAssetAvailableLocale);
                    }
                }

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.Error($"Error processing AssetAvailableLocale. AssetId: {assetId} " + ex.Message);
                throw;
            }
        }
    }
}
