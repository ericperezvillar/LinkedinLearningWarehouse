using AutoMapper;
using LinkedinLearningWarehouse.DTOs.LearningActivity;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;

namespace LinkedinLearningWarehouse.Mappers.Resolvers
{
    public class AssetTypeResolver : IValueResolver<object, object, int?>
    {
        private readonly IAssetTypeService _assetTypeService;

        public AssetTypeResolver(IAssetTypeService assetTypeService)
        {
            _assetTypeService = assetTypeService;
        }

        public int? Resolve(object source, object destination, int? destMember, ResolutionContext context)
        {
            string? assetType = null;

            if (source is ActivityDto activityDto)
            {
                assetType = activityDto.AssetType;
            }
            else if (source is RootObjectLearningAsset rootObjectLearningAsset)
            {
                assetType = rootObjectLearningAsset.Type;
            }
            else if (source is AssetDto assetDto)
            {
                assetType = assetDto.Type;
            }

            return assetType != null ? _assetTypeService.GetAssetTypeIdAsync(assetType).Result : null;
        }
    }
}
