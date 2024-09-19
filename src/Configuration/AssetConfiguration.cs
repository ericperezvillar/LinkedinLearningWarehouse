using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> entity)
        {
            entity.ToTable("Asset").HasKey(e => e.AssetId);

            entity.HasOne(a => a.AssetDetail)
               .WithOne(a => a.Asset)
               .HasForeignKey<AssetDetail>(e => e.AssetId);

            entity.HasOne<AssetType>()
               .WithMany()
               .HasForeignKey(e => e.AssetTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.AssetId).HasColumnName("asset_id");

            entity.Property(e => e.Urn).HasColumnName("urn").IsRequired(); 

            entity.Property(e => e.Title).HasColumnName("title").IsRequired();

            entity.Property(e => e.TitleCountry).HasColumnName("title_country");

            entity.Property(e => e.TitleLanguage).HasColumnName("title_language");                       

            entity.Property(e => e.AssetTypeId).HasColumnName("asset_type_id");

        }
    }
}
