using Newtonsoft.Json;
namespace LinkedinLearningWarehouse.DTOs.LearningActivity
{
    public class LearnerDetailsDto
    {
        public string Name { get; set; }
        public List<string> EnterpriseGroups { get; set; }

        [JsonProperty("entity")]
        public EntityDto EntityDto { get; set; }

        public string Email { get; set; }
        public string UniqueUserId { get; set; }
    }
}
