using LinkedinLearningWarehouse.Models.LearningAsset;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class ClassificationDetailConfiguration : IEntityTypeConfiguration<ClassificationDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ClassificationDetail> entity)
        {
            entity.ToTable("ClassificationDetail").HasKey(c => c.ClassificationId);

            entity.HasOne<ClassificationType>()
                .WithMany()
                .HasForeignKey(e => e.ClassificationTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<OwnerDetail>()
                .WithMany()
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.ClassificationId).HasColumnName("classification_id");

            entity.Property(e => e.ClassificationTypeId).HasColumnName("classification_type_id");

            entity.Property(e => e.OwnerId).HasColumnName("owner_id").IsRequired();

            entity.Property(e => e.Country).HasColumnName("country");

            entity.Property(e => e.Language).HasColumnName("language");

            entity.Property(e => e.Name).HasColumnName("name_value");

            entity.Property(e => e.Urn).HasColumnName("urn").IsRequired();
        }
    }
}
