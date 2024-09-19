using Newtonsoft.Json;

namespace LinkedinLearningWarehouse.DTOs.LearningActivity
{
    public class ElementDto
    {
        [JsonProperty("learnerDetails")]
        public LearnerDetailsDto LearnerDetailsDto { get; set; }

        [JsonProperty("activities")]
        public List<ActivityDto> ActivitiesDtos { get; set; }

        [JsonProperty("contentDetails")]
        public ContentDetailsDto ContentDetailsDto { get; set; }
    }
}
