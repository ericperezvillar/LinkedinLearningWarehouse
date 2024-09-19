using Newtonsoft.Json;

namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class AssetUrlsDto
    {
        [JsonProperty("aiccLaunch")]
        public string AiccLaunchUrl { get; set; }

        [JsonProperty("ssoLaunch")]
        public string SsoLaunchUrl { get; set; }

        [JsonProperty("webLaunch")]
        public string WebLaunchUrl { get; set; }
    }
}