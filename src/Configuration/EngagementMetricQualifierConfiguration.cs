using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningActivitity;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class EngagementMetricQualifierConfiguration : IEntityTypeConfiguration<EngagementMetricQualifier>
    {
        public void Configure(EntityTypeBuilder<EngagementMetricQualifier> entity)
        {
            entity.ToTable("EngagementMetricQualifier").HasKey(e => e.MetricQualifierId);            
            entity.Property(e => e.MetricQualifierId).HasColumnName("metric_qualifier_id");
            entity.Property(e => e.MetricQualifierName).HasColumnName("metric_qualifier_name");
        }
    }
}
