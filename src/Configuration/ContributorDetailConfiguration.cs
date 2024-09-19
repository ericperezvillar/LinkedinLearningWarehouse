using LinkedinLearningWarehouse.Models.LearningActivitity;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class ContributorDetailConfiguration : IEntityTypeConfiguration<ContributorDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ContributorDetail> entity)
        {
            entity.ToTable("ContributorDetail").HasKey(c => c.ContributorId);

            entity.HasOne<ContributorType>()
                .WithMany()
                .HasForeignKey(e => e.ContributorTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.ContributorId).HasColumnName("contributor_id");

            entity.Property(e => e.ContributorTypeId).HasColumnName("contributor_type_id");

            entity.Property(e => e.Urn).HasColumnName("urn").IsRequired();

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.AuthorFirstName).HasColumnName("author_first_name");

            entity.Property(e => e.AuthorLastName).HasColumnName("author_last_name");
        }
    }
}
