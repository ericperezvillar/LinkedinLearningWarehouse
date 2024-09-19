using Newtonsoft.Json;

namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class ClassificationDetailDto
    {
        public NameDto Assigner { get; set; }

        [JsonProperty("path")]
        public List<AssetAssociatedClassificationDto> PathDto { get; set; }

        [JsonProperty("associatedClassification")]
        public AssetAssociatedClassificationDto AssociatedClassificationDto { get; set; }
    }
}