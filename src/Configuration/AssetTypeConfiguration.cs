using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetTypeConfiguration : IEntityTypeConfiguration<AssetType>
    {
        public void Configure(EntityTypeBuilder<AssetType> entity)
        {
            entity.ToTable("AssetType").HasKey(e => e.AssetTypeId);
            entity.Property(e => e.AssetTypeId).HasColumnName("asset_type_id");
            entity.Property(e => e.AssetTypeName).HasColumnName("asset_type_name");
            entity.Property(e => e.Active).HasColumnName("active");
        }
    }
}
