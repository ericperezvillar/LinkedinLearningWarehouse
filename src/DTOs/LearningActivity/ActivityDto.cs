namespace LinkedinLearningWarehouse.DTOs.LearningActivity
{
    public class ActivityDto
    {
        public string EngagementType { get; set; }
        public long LastEngagedAt { get; set; }
        public long FirstEngagedAt { get; set; }
        public string AssetType { get; set; }
        public string EngagementMetricQualifier { get; set; }
        public int EngagementValue { get; set; }
    }
}
