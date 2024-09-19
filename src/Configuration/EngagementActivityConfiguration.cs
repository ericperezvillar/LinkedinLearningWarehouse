using LinkedinLearningWarehouse.Models.LearningActivitity;
using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class EngagementActivityConfiguration : IEntityTypeConfiguration<EngagementActivity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EngagementActivity> entity)
        {
            entity.ToTable("EngagementActivity").HasKey(e => e.EngagementActivityId);

            entity.HasOne<LearnerDetail>()
                .WithMany()
                .HasForeignKey(e => e.LearnerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<AssetType>()
                .WithMany()
                .HasForeignKey(e => e.AssetTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<EngagementMetricType>()
                .WithMany()
                .HasForeignKey(e => e.MetricTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<EngagementMetricQualifier>()
                .WithMany()
                .HasForeignKey(e => e.MetricQualifierId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure columns
            entity.Property(e => e.EngagementActivityId).HasColumnName("engagement_activity_id").IsRequired();

            entity.Property(e => e.LearnerId).HasColumnName("learner_id").IsRequired();

            entity.Property(e => e.ContentId).HasColumnName("content_id");

            entity.Property(e => e.AssetTypeId).HasColumnName("asset_type_id");

            entity.Property(e => e.MetricTypeId).HasColumnName("metric_type_id").IsRequired();

            entity.Property(e => e.MetricQualifierId).HasColumnName("metric_qualifier_id").IsRequired();

            entity.Property(e => e.EngagementValue).HasColumnName("engagement_value").IsRequired();

            entity.Property(e => e.FirstEngagedAt).HasColumnName("first_engaged_at");

            entity.Property(e => e.LastEngagedAt).HasColumnName("last_engaged_at");
        }
    }
}
