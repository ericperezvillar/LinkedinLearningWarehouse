using Newtonsoft.Json;

namespace LinkedinLearningWarehouse.DTOs.LearningActivity
{
    public class ContentDetailsDto
    {
        public string ContentProviderName { get; set; }
        public string Name { get; set; }
        public string ContentUrn { get; set; }

        [JsonProperty("locale")]
        public LocaleDto LocaleDto { get; set; }
    }
}
