using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetClassificationPathConfiguration : IEntityTypeConfiguration<AssetClassificationPath>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AssetClassificationPath> entity)
        {
            entity.ToTable("AssetClassificationPath").HasKey(c => c.AssetClassificationPathId);

            entity.HasOne<ClassificationDetail>()
                .WithMany()
                .HasForeignKey(e => e.ClassificationId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne<AssetClassification>()
                .WithMany()
                .HasForeignKey(e => e.AssetClassificationId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.Property(e => e.AssetClassificationPathId).HasColumnName("asset_classification_path_id");

            entity.Property(e => e.AssetClassificationId).HasColumnName("asset_classification_id").IsRequired();

            entity.Property(e => e.ClassificationId).HasColumnName("classification_id").IsRequired();
        }
    }
}
