namespace LinkedinLearningWarehouse.Models.LearningActivitity
{
    public class EngagementActivity
    {
        public int EngagementActivityId { get; set; }
        public int LearnerId { get; set; }
        public int ContentId { get; set; }
        public int? AssetTypeId { get; set; }
        public int MetricTypeId { get; set; }
        public int MetricQualifierId { get; set; }
        public int EngagementValue { get; set; }
        public DateTime? FirstEngagedAt { get; set; }
        public DateTime? LastEngagedAt { get; set; }
    }

}
