using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningActivitity;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class EngagementMetricTypeConfiguration : IEntityTypeConfiguration<EngagementMetricType>
    {
        public void Configure(EntityTypeBuilder<EngagementMetricType> entity)
        {
            entity.ToTable("EngagementMetricType").HasKey(e => e.MetricTypeId);
            entity.Property(e => e.MetricTypeId).HasColumnName("metric_type_id");
            entity.Property(e => e.MetricTypeName).HasColumnName("metric_type_name");
        }
    }
}
