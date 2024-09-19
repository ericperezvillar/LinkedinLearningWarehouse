namespace LinkedinLearningWarehouse.Models.LearningAsset
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string Urn { get; set; }
        public string Title { get; set; }
        public string TitleCountry { get; set; }
        public string TitleLanguage { get; set; }        
        public int? AssetTypeId { get; set; }

        // Collection navigation properties
        public ICollection<AssetContent> ParentAssetContents { get; set; } = new List<AssetContent>();
        public ICollection<AssetContent> ChildAssetContents { get; set; } = new List<AssetContent>();
        public AssetDetail? AssetDetail { get; set; }
    }
}
