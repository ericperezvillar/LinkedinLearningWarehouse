using AutoMapper;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Identity;
using LinkedinLearningWarehouse.Interfaces;
using LinkedinLearningWarehouse.Interfaces.Client;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace LinkedinLearningWarehouse.Services
{
    public class LearningAssetsService : ILearningAssetsService
    {
        private readonly ILinkedInApiClientService<RootObjectLearningAsset> _linkedInApiClientService;
        private readonly IAssetClassificationService _assetClassificationService;
        private readonly IAssetContentService _assetContentService;
        private readonly IAssetUrlService _assetUrlService;
        private readonly IAssetAvailableLocaleService _assetAvailableLocaleService;
        private readonly IAssetContributorService _assetContributorService;
        private readonly IAssetService _assetService;
        private readonly AppSettings _appSettings;
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LearningAssetsService(ILinkedInApiClientService<RootObjectLearningAsset> linkedInApiClientService,                                      
                                     LinkedinLearningDbContext dbContext, 
                                     IMapper mapper,
                                     IAssetClassificationService assetClassificationService,
                                     IAssetUrlService assetUrlService,
                                     IAssetAvailableLocaleService assetAvailableLocaleService,
                                     IAssetContributorService assetContributorService,
                                     IAssetContentService assetContentService,
                                     IAssetService assetService,
                                     IOptions<AppSettings> appSettings)
        {
            _logger = Log.ForContext<LearningAssetsService>();
            _linkedInApiClientService = linkedInApiClientService;
            _dbContext = dbContext;
            _mapper = mapper;
            _assetClassificationService = assetClassificationService;
            _assetUrlService = assetUrlService;
            _assetAvailableLocaleService = assetAvailableLocaleService;
            _assetContributorService = assetContributorService;
            _assetContentService = assetContentService;
            _assetService = assetService;
            _appSettings = appSettings.Value;
        }

        public async Task PopulateLearningAssets(OAuthTokenResponse tokenResponse, List<string> contentUrns)
        {
            foreach (var urn in contentUrns)
            {
                try
                {
                    var requestUrl = $"{_appSettings.LearningAssetsUrl}/{urn}";

                    var result = await _linkedInApiClientService.GetJsonResponse(tokenResponse, requestUrl);

                    await SaveDataAsync(result);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error processing Learning Asset - Urn {urn} - Error: {ex.Message}");
                }
            }
        }

        private async Task SaveDataAsync(RootObjectLearningAsset data)
        {
            try
            {
                var existingAsset = await _assetService.GetAsset(data.Urn);

                int assetId = 0;

                if (existingAsset != null)
                {
                    assetId = existingAsset.AssetId;

                    var existingAssetDetail = await _dbContext.AssetDetails
                       .FirstOrDefaultAsync(ac => ac.AssetId == existingAsset.AssetId);

                    if (existingAssetDetail != null)
                    {
                        existingAssetDetail.AssetId = assetId;
                        _mapper.Map(data.Details, existingAssetDetail);
                        //_dbContext.AssetDetails.Update(existingAssetDetail);
                    }
                    else
                    {
                        var newAssetDetail = _mapper.Map<AssetDetail>(data.Details);
                        await _dbContext.AssetDetails.AddAsync(newAssetDetail);
                        newAssetDetail.AssetId = assetId;
                    }

                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    assetId = await _assetService.CreateAssetFromRoot(data);
                    var newAssetDetail = _mapper.Map<AssetDetail>(data.Details);
                    await _dbContext.AssetDetails.AddAsync(newAssetDetail);
                    newAssetDetail.AssetId = assetId;
                }


                await _dbContext.SaveChangesAsync();

                var assetContents = _mapper.Map<List<AssetContent>>(data.Contents);

                if (data.Contents != null)
                {
                    foreach (var assetContent in data.Contents)
                    {
                        await _assetContentService.CreateAssetContent(assetContent, assetId);
                    }
                }

                await _assetUrlService.CreateOrUpdateAssetUrl(data.Details.UrlsDto, assetId);

                await _assetAvailableLocaleService.CreateAssetAvailableLocale(data.Details.AvailableLocalesDto, assetId) ;

                if (data.Details.AssetClassificationsDto != null)
                {
                    foreach (var assetClassificationDto in data.Details.AssetClassificationsDto)
                    {
                        await _assetClassificationService.CreateOrUpdateAssetClassificationAsync(assetClassificationDto, assetId);
                    }
                }

                if (data.Details.ContributorsDto != null)
                {
                    foreach (var contributor in data.Details.ContributorsDto)
                    {
                        await _assetContributorService.CreateOrUpdateAssetContributor(contributor, assetId);
                    }
                }

                await _dbContext.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                _logger.Error($"Error saving data in DB with URN {data.Urn}. {ex.Message}");
                throw;
            }                       
        }
    }
}
