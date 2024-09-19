using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class OwnerDetailConfiguration : IEntityTypeConfiguration<OwnerDetail>
    {
        public void Configure(EntityTypeBuilder<OwnerDetail> entity)
        {
            entity.ToTable("OwnerDetail").HasKey(o => o.OwnerId);
            entity.Property(c => c.OwnerId).HasColumnName("owner_id");

            entity.Property(o => o.Name)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(o => o.Urn)
                  .HasMaxLength(255);

            entity.Property(o => o.Country)
                  .HasMaxLength(2);

            entity.Property(o => o.Language)
                  .HasMaxLength(2);
        }
    }
}
