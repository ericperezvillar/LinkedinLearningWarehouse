using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningActivitity;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class ContributorTypeConfiguration : IEntityTypeConfiguration<ContributorType>
    {
        public void Configure(EntityTypeBuilder<ContributorType> entity)
        {
            entity.ToTable("ContributorType").HasKey(e => e.ContributorTypeId);
            entity.Property(e => e.ContributorTypeId).HasColumnName("contributor_type_id");
            entity.Property(e => e.Name).HasColumnName("name");
        }
    }
}
