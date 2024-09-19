namespace LinkedinLearningWarehouse.Models.LearningAsset
{
    public class AssetUrl
    {
        public int AssetUrlId { get; set; }
        public int AssetId { get; set; }
        public string? AiccLaunchUrl { get; set; }
        public string? SsoLaunchUrl { get; set; }
        public string? WebLaunchUrl { get; set; }
    }
}
