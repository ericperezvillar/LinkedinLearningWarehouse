using Microsoft.Extensions.Options;

namespace LinkedinLearningWarehouse
{
    public class AppSettings : IOptions<AppSettings>
    {
        public string? LearningActivityUrl { get; set; }       
        public int TimeBetweenCallsInMill { get; set; }
        public string? LearningAssetsUrl { get; set; }
        public string? TokenUrl { get; set; }
        public string? TokenGrantType { get; set; }
        public AppSettings Value => this;
    }
}
