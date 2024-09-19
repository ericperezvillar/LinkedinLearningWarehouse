using Newtonsoft.Json;

namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class AssetDetailDto
    {
        public long LastUpdatedAt { get; set; }

        public long PublishedAt { get; set; }

        [JsonProperty("discoverableBy")]
        public List<AssetDiscoverableByDto> DiscoverableByDto { get; set; }

        [JsonProperty("description")]
        public AssetDescriptionDto DescriptionDto { get; set; }

        [JsonProperty("shorDescription")]
        public AssetDescriptionDto ShortDescriptionDto { get; set; }

        public string Availability { get; set; }

        public string Level { get; set; }

        [JsonProperty("availableLocales")]
        public List<LocaleDto> AvailableLocalesDto { get; set; }

        [JsonProperty("classifications")]
        public List<AssetClassificationDto> AssetClassificationsDto { get; set; }

        [JsonProperty("urls")]
        public AssetUrlsDto UrlsDto { get; set; }

        [JsonProperty("contributors")]
        public List<ContributorDetailDto> ContributorsDto { get; set; }

        [JsonProperty("timeToComplete")]
        public TimeToCompleteDto TimeToCompleteDto { get; set; }
    }
}
