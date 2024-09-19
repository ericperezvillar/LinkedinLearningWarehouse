using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class ClassificationTypeConfiguration : IEntityTypeConfiguration<ClassificationType>
    {
        public void Configure(EntityTypeBuilder<ClassificationType> entity)
        {
            entity.ToTable("ClassificationType").HasKey(e => e.ClassificationTypeId);
            entity.Property(e => e.ClassificationTypeId).HasColumnName("classification_type_id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired();
        }
    }
}
