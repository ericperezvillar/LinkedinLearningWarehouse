using LinkedinLearningWarehouse.Models.LearningActivitity;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class LearnerDetailConfiguration : IEntityTypeConfiguration<LearnerDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LearnerDetail> entity)
        {
            entity.ToTable("LearnerDetail").HasKey(c => c.LearnerId );
            entity.Property(c => c.LearnerId).HasColumnName("learner_id");
            entity.Property(c => c.Name);
            entity.Property(c => c.Email);
            entity.Property(c => c.ProfileUrn).HasColumnName("profile_urn");
            entity.Property(c => c.UniqueUserId).HasColumnName("unique_user_id");
        }
    }
}
