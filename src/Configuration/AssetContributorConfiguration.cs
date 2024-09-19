using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningActivitity;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetContributorConfiguration : IEntityTypeConfiguration<AssetContributor>
    {
        public void Configure(EntityTypeBuilder<AssetContributor> entity)
        {
            entity.ToTable("AssetContributor").HasKey(e => e.AssetContributorId);

            entity.HasOne<AssetDetail>()
               .WithMany()
               .HasForeignKey(e => e.AssetId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<ContributorDetail>()
               .WithMany()
               .HasForeignKey(e => e.ContributorId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.AssetContributorId).HasColumnName("asset_contributor_id");
            entity.Property(e => e.AssetId).HasColumnName("asset_id");
            entity.Property(e => e.ContributorId).HasColumnName("contributor_id");
        }
    }
}
