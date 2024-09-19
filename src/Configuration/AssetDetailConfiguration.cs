using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetDetailConfiguration : IEntityTypeConfiguration<AssetDetail>
    {
        public void Configure(EntityTypeBuilder<AssetDetail> entity)
        {
            entity.ToTable("AssetDetail").HasKey(e => e.AssetId);//.HasBaseType<Asset>(); 

            entity.HasOne(a => a.Asset)
                .WithOne(a => a.AssetDetail)
                .HasForeignKey<AssetDetail>(e => e.AssetId)
                .IsRequired();

            entity.Property(e => e.AssetId).HasColumnName("asset_id");

            entity.Property(e => e.Description).HasColumnName("description");

            entity.Property(e => e.ShortDescription).HasColumnName("short_description");

            entity.Property(e => e.AccessorName).HasColumnName("accessor_name");

            entity.Property(e => e.AccessorUrn).HasColumnName("accessor_urn");

            entity.Property(e => e.Availability).HasColumnName("availability");

            entity.Property(e => e.Level).HasColumnName("level");

            entity.Property(e => e.TimeToCompleteDuration).HasColumnName("time_to_complete_duration");

            entity.Property(e => e.TimeToCompleteUnit).HasColumnName("time_to_complete_unit");

            entity.Property(e => e.LastUpdatedAt).HasColumnName("last_updated_at");

            entity.Property(e => e.PublishedAt).HasColumnName("published_at");
        }
    }
}
