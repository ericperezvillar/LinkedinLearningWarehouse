namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class ContributorDetailDto
    {
        public NameDto Name { get; set; }
        public string Urn { get; set; }
        public string ContributionType { get; set; }
        public AuthorDetailsDto AuthorDetails { get; set; }
    }
}