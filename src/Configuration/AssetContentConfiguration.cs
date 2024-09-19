using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetContentConfiguration : IEntityTypeConfiguration<AssetContent>
    {
        public void Configure(EntityTypeBuilder<AssetContent> entity)
        {
            entity.ToTable("AssetContent");

            // Configure composite primary key
            entity.HasKey(ac => new { ac.ParentAssetId, ac.ChildAssetId });

            entity.Property(e => e.ParentAssetId).HasColumnName("parent_asset_id");
            entity.Property(e => e.ChildAssetId).HasColumnName("child_asset_id");

            entity.HasOne(ac => ac.ParentAsset)
               .WithMany(a => a.ParentAssetContents)
               .HasForeignKey(ac => ac.ParentAssetId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ac => ac.ChildAsset)
                   .WithMany(a => a.ChildAssetContents)
                   .HasForeignKey(ac => ac.ChildAssetId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
