using Newtonsoft.Json;

namespace LinkedinLearningWarehouse.DTOs.LearningActivity
{
    public class PagingDto
    {
        public int Start { get; set; }
        public int Count { get; set; }

        [JsonProperty("links")]
        public List<LinkDto> LinksDtos { get; set; }

        public int Total { get; set; }
    }
}
