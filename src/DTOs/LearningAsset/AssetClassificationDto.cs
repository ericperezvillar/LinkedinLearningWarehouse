using Newtonsoft.Json;

namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class AssetClassificationDto
    {
        public OwnerDto Assigner { get; set; }

        [JsonProperty("path")]
        public List<ClassificationDto> PathDto { get; set; }

        [JsonProperty("associatedClassification")]
        public ClassificationDto AssociatedClassificationDto { get; set; }
    }
}