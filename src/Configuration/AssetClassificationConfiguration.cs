using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class AssetClassificationConfiguration : IEntityTypeConfiguration<AssetClassification>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AssetClassification> entity)
        {
            entity.ToTable("AssetClassification").HasKey(c => c.AssetClassificationId);

            entity.HasOne<ClassificationDetail>()
                .WithMany()
                .HasForeignKey(e => e.ClassificationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<AssetDetail>()
                .WithMany()
                .HasForeignKey(e => e.AssetId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.AssetClassificationId).HasColumnName("asset_classification_id");

            entity.Property(e => e.ClassificationId).HasColumnName("classification_id").IsRequired();

            entity.Property(e => e.AssetId).HasColumnName("asset_id").IsRequired();

            entity.Property(e => e.AssignerName).HasColumnName("assigner_name");

            entity.Property(e => e.AssignerUrn).HasColumnName("assigner_urn");
        }
    }
}
