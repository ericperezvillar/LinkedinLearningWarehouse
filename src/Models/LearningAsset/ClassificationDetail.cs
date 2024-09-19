namespace LinkedinLearningWarehouse.Models.LearningAsset
{
    public class ClassificationDetail
    {
        public int ClassificationId { get; set; }
        public int? ClassificationTypeId { get; set; }
        public int? OwnerId { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Urn { get; set; }
    }
}
