using AutoMapper;
using LinkedinLearningWarehouse.DTOs;
using LinkedinLearningWarehouse.DTOs.LearningActivity;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Mappers.Resolvers;
using LinkedinLearningWarehouse.Models.LearningActivitity;
using LinkedinLearningWarehouse.Models.LearningAsset;
using LinkedinLearningWarehouse.Utility;

namespace LinkedinLearningWarehouse.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // LearnerDetail Mapping
            CreateMap<LearnerDetailsDto, LearnerDetail>()
                .ForMember(dest => dest.LearnerId, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileUrn, opt => opt.MapFrom(src => src.EntityDto.ProfileUrn))
                .ForMember(dest => dest.UniqueUserId, opt => opt.MapFrom(src => src.UniqueUserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            // EnterpriseGroup Mapping
            CreateMap<string, EnterpriseGroup>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.LearnerId, opt => opt.Ignore()); // Will be set after mapping

            // ContentDetail Mapping
            CreateMap<ContentDetailsDto, ContentDetail>()
                .ForMember(dest => dest.ContentId, opt => opt.Ignore()) // Assuming ContentId is auto-generated
                .ForMember(dest => dest.ContentProviderName, opt => opt.MapFrom(src => src.ContentProviderName))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ContentUrn, opt => opt.MapFrom(src => src.ContentUrn))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.LocaleDto.Country))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.LocaleDto.Language));

            CreateMap<ActivityDto, EngagementActivity>()
                .ForMember(dest => dest.EngagementActivityId, opt => opt.Ignore())
                .ForMember(dest => dest.EngagementValue, opt => opt.MapFrom(src => src.EngagementValue))
                .ForMember(dest => dest.AssetTypeId, opt => opt.MapFrom<AssetTypeResolver>())
                .ForMember(dest => dest.MetricTypeId, opt => opt.MapFrom<EngagementMetricTypeResolver>())
                .ForMember(dest => dest.MetricQualifierId, opt => opt.MapFrom<EngagementMetricQualifierResolver>())
                .ForMember(dest => dest.FirstEngagedAt, opt => opt.MapFrom(src => TransformJsonDataHelper.UnixTimeStampToDateTime(src.FirstEngagedAt)))
                .ForMember(dest => dest.LastEngagedAt, opt => opt.MapFrom(src => TransformJsonDataHelper.UnixTimeStampToDateTime(src.LastEngagedAt)));

            // AssetUrl Mapping
            CreateMap<AssetUrlsDto, AssetUrl>();

            // AssetUrl Mapping
            CreateMap<OwnerDto, OwnerDetail>()
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Urn, opt => opt.MapFrom(src => src.Urn))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Name.Locale.Language))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Name.Locale.Country));

            CreateMap<ClassificationDto, ClassificationDetail>()
                .ForMember(dest => dest.ClassificationId, opt => opt.Ignore())
                .ForMember(dest => dest.ClassificationTypeId, (src => src.MapFrom<ClassificationTypeResolver>()))
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Urn, opt => opt.MapFrom(src => src.Urn))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Name.Locale.Language))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Name.Locale.Country));

            // AssetClassification Mapping
            CreateMap<AssetClassificationDto, AssetClassification>()
               .ForMember(dest => dest.AssetClassificationId, opt => opt.Ignore())
               .ForMember(dest => dest.ClassificationId, opt => opt.Ignore())
               .ForMember(dest => dest.AssetId, opt => opt.Ignore()) // Will be set after mapping
               .ForMember(dest => dest.AssignerUrn, opt => opt.MapFrom(src => src.AssociatedClassificationDto.Owner.Urn))
               .ForMember(dest => dest.AssignerName, opt => opt.MapFrom(src => src.AssociatedClassificationDto.Owner.Name.Value));

            // AssetClassification Mapping
            CreateMap<LocaleDto, AssetAvailableLocale>()
               .ForMember(dest => dest.AssetAvailableLocaleId, opt => opt.Ignore())
               .ForMember(dest => dest.AssetId, opt => opt.Ignore())
               .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
               .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language));

            CreateMap<AssetDto, Asset>()
                .ForMember(dest => dest.AssetId, opt => opt.Ignore())
                .ForMember(dest => dest.Urn, opt => opt.MapFrom(src => src.Urn))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
                .ForMember(dest => dest.TitleCountry, opt => opt.MapFrom(src => src.Title.Locale.Country))
                .ForMember(dest => dest.TitleLanguage, opt => opt.MapFrom(src => src.Title.Locale.Language))
                .ForMember(dest => dest.AssetTypeId, opt => opt.MapFrom<AssetTypeResolver>())
                .ForMember(dest => dest.ChildAssetContents, opt => opt.Ignore())
                .ForMember(dest => dest.ParentAssetContents, opt => opt.Ignore()); 

            CreateMap<RootObjectLearningAsset, Asset>()
                .ForMember(dest => dest.AssetId, opt => opt.Ignore())
                .ForMember(dest => dest.Urn, opt => opt.MapFrom(src => src.Urn))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
                .ForMember(dest => dest.TitleCountry, opt => opt.MapFrom(src => src.Title.Locale.Country))
                .ForMember(dest => dest.TitleLanguage, opt => opt.MapFrom(src => src.Title.Locale.Language))
                .ForMember(dest => dest.AssetTypeId, opt => opt.MapFrom<AssetTypeResolver>())
                .ForMember(dest => dest.ChildAssetContents, opt => opt.Ignore())
                .ForMember(dest => dest.ParentAssetContents, opt => opt.Ignore()); 


            // Map AssetContentDto to AssetContent
            CreateMap<AssetContentDto, AssetContent>()
                .ForMember(dest => dest.ParentAssetId, opt => opt.Ignore()) 
                .ForMember(dest => dest.ChildAssetId, opt => opt.Ignore())
                .ForMember(dest => dest.ParentAsset, opt => opt.Ignore())
                .ForMember(dest => dest.ChildAsset, opt => opt.Ignore());
          
            // Mapping from DTO to Entity
            CreateMap<AssetDetailDto, AssetDetail>()
                .ForMember(dest => dest.AssetId, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DescriptionDto.Value))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescriptionDto.Value))
                // DiscoverableBy not in the API documentation, hence we take only the first record as reference
                .ForMember(dest => dest.AccessorName, opt => opt.MapFrom(src => src.DiscoverableByDto != null && src.DiscoverableByDto.Any() ? src.DiscoverableByDto[0].AccessorsDto.Name : null))
                .ForMember(dest => dest.AccessorUrn, opt => opt.MapFrom(src => src.DiscoverableByDto != null && src.DiscoverableByDto.Any() ? src.DiscoverableByDto[0].AccessorsDto.Urn : null))
                .ForMember(dest => dest.Availability, opt => opt.MapFrom(src => src.Availability))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.TimeToCompleteUnit, opt => opt.MapFrom(src => src.TimeToCompleteDto != null ? src.TimeToCompleteDto.Unit : null))
                .ForMember(dest => dest.TimeToCompleteDuration, opt => opt.MapFrom(src => src.TimeToCompleteDto != null ? src.TimeToCompleteDto.Duration : (int?)null))
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.MapFrom(src => TransformJsonDataHelper.UnixTimeStampToDateTime(src.LastUpdatedAt)))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => TransformJsonDataHelper.UnixTimeStampToDateTime(src.PublishedAt)));


            // ContributorDetail Mapping
            CreateMap<ContributorDetailDto, ContributorDetail>()
                .ForMember(dest => dest.ContributorId, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Urn, opt => opt.MapFrom(src => src.Urn))
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Name.Locale.Language))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Name.Locale.Country))
                .ForMember(dest => dest.ContributorTypeId, opt => opt.MapFrom<ContributorTypeResolver>());


        }
    }
}
