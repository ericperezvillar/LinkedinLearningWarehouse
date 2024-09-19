namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class RootObjectLearningAsset
    {
        public string Urn { get; set; }
        public AssetDetailDto Details { get; set; }
        public AssetDescriptionDto Title { get; set; }
        public string Type { get; set; }
        public List<AssetContentDto> Contents { get; set; }
    }
}
