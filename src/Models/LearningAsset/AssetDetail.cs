namespace LinkedinLearningWarehouse.Models.LearningAsset
{
    public class AssetDetail //: Asset
    {
        public int AssetId { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? AccessorName { get; set; }
        public string? AccessorUrn { get; set; }
        public string? Availability { get; set; }
        public string? Level { get; set; }
        public string? TimeToCompleteUnit { get; set; }
        public int? TimeToCompleteDuration { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime PublishedAt { get; set; }

        public Asset Asset { get; set; } = null!;
    }
}
