namespace LinkedinLearningWarehouse.Models.LearningAsset
{
    public class AssetClassification
    {
        public int AssetClassificationId { get; set; }
        public int ClassificationId { get; set; }
        public int AssetId { get; set; }
        public string AssignerUrn { get; set; }
        public string AssignerName { get; set; }
    }
}
