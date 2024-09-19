using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetAvailableLocaleConfiguration : IEntityTypeConfiguration<AssetAvailableLocale>
    {
        public void Configure(EntityTypeBuilder<AssetAvailableLocale> entity)
        {
            entity.ToTable("AssetAvailableLocale").HasKey(e => e.AssetAvailableLocaleId);

            entity.HasOne<AssetDetail>()
               .WithMany()
               .HasForeignKey(e => e.AssetId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.AssetAvailableLocaleId).HasColumnName("asset_available_locale_id");

            entity.Property(e => e.AssetId).HasColumnName("asset_id").IsRequired(); 

            entity.Property(e => e.Country).HasColumnName("country");

            entity.Property(e => e.Language).HasColumnName("language");
        }
    }
}
