namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class AssetDto
    {
        public string Urn { get; set; }
        public AssetDescriptionDto Title { get; set; } 
        public string Type { get; set; }
        public List<AssetContentDto> Contents { get; set; } = new List<AssetContentDto>();
    }
}
