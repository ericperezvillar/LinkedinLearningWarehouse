namespace LinkedinLearningWarehouse.Models.LearningAsset
{
    public class AssetContent
    {
        public int ParentAssetId { get; set; }
        public int ChildAssetId { get; set; }

        // Navigation properties to define relationships
        public Asset ParentAsset { get; set; } = null!;
        public Asset ChildAsset { get; set; } = null!;
    }
}
