using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningActivitity;
namespace LinkedinLearningWarehouse.Configuration
{
    internal class EnterpriseGroupConfiguration : IEntityTypeConfiguration<EnterpriseGroup>
    {
        public void Configure(EntityTypeBuilder<EnterpriseGroup> entity)
        {
            entity.ToTable("EnterpriseGroup").HasKey(e => e.GroupId);
            entity.Property(c => c.GroupId).HasColumnName("group_id");
            entity.Property(c => c.GroupName).HasColumnName("group_name");
            entity.Property(c => c.LearnerId).HasColumnName("learner_id");
        }
    }
}
