namespace LinkedinLearningWarehouse.Models.LearningActivitity
{
    public class LearnerDetail
    {
        public int LearnerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? UniqueUserId { get; set; }
        public string? ProfileUrn { get; set; }
    }
}
