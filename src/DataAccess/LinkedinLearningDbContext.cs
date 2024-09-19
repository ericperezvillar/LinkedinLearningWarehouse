using LinkedinLearningWarehouse.Models;
using LinkedinLearningWarehouse.Models.LearningActivitity;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LinkedinLearningWarehouse.DataAccess
{
    public class LinkedinLearningDbContext : DbContext
    {
        public LinkedinLearningDbContext(DbContextOptions<LinkedinLearningDbContext> options) : base(options) { }

        public DbSet<LearnerDetail> LearnerDetails { get; set; }
        public DbSet<EnterpriseGroup> EnterpriseGroups { get; set; }
        public DbSet<EngagementMetricQualifier> EngagementMetricQualifiers { get; set; }
        public DbSet<EngagementActivity> EngagementActivities { get; set; }
        public DbSet<ContentDetail> ContentDetails { get; set; }
        public DbSet<OwnerDetail> OwnerDetails { get; set; }
        public DbSet<ClassificationDetail> ClassificationDetails { get; set; }
        public DbSet<EngagementMetricType> EngagementMetricTypes { get; set; }
        public DbSet<QueryParameter> QueryParameters { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<ClassificationType> ClassificationTypes { get; set; }
        public DbSet<IntegratorHistory> IntegratorHistories { get; set; }
        public DbSet<AssetUrl> AssetUrls { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetDetail> AssetDetails { get; set; }
        public DbSet<AssetContent> AssetContents { get; set; }
        public DbSet<AssetClassification> AssetClassifications { get; set; }
        public DbSet<AssetClassificationPath> AssetClassificationPaths { get; set; }
        public DbSet<AssetAvailableLocale> AssetAvailableLocales { get; set; }
        public DbSet<ContributorDetail> ContributorDetails { get; set; }
        public DbSet<ContributorType> ContributorTypes { get; set; }
        public DbSet<AssetContributor> AssetContributors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
