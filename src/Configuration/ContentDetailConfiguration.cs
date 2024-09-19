using LinkedinLearningWarehouse.Models.LearningActivitity;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class ContentDetailConfiguration : IEntityTypeConfiguration<ContentDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ContentDetail> entity)
        {
            entity.ToTable("ContentDetail").HasKey(c =>  c.ContentId);

            entity.Property(cd => cd.ContentId).HasColumnName("content_id");

            entity.Property(cd => cd.ContentProviderName).HasColumnName("content_provider_name");

            entity.Property(cd => cd.Name).HasColumnName("name");

            entity.Property(cd => cd.ContentUrn).HasColumnName("content_urn");

            entity.Property(cd => cd.Country).HasColumnName("country").HasMaxLength(2).IsRequired();

            entity.Property(cd => cd.Language).HasColumnName("language").HasMaxLength(10).IsRequired();
        }
    }
}
