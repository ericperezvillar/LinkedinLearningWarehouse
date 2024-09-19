namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class AssetAssociatedClassificationDto
    {
        public OwnerDto Owner { get; set; }
        public NameDto Name { get; set; }
        public string Urn { get; set; }
        public string Type { get; set; }
    }
}