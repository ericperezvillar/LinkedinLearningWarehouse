using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetUrlConfiguration : IEntityTypeConfiguration<AssetUrl>
    {
        public void Configure(EntityTypeBuilder<AssetUrl> entity)
        {
            entity.ToTable("AssetUrl").HasKey(e => e.AssetUrlId);

            entity.HasOne<AssetUrl>()
               .WithMany()
               .HasForeignKey(e => e.AssetId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.AssetUrlId).HasColumnName("asset_url_id");

            entity.Property(e => e.AssetId).HasColumnName("asset_id").IsRequired(); 

            entity.Property(e => e.AiccLaunchUrl).HasColumnName("aicc_launch");

            entity.Property(e => e.SsoLaunchUrl).HasColumnName("sso_launch");

            entity.Property(e => e.WebLaunchUrl).HasColumnName("web_launch");
        }
    }
}
