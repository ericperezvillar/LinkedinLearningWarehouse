using LinkedinLearningWarehouse.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class QueryParameterConfiguration : IEntityTypeConfiguration<QueryParameter>
    {
        public void Configure(EntityTypeBuilder<QueryParameter> entity)
        {
            entity.ToTable("QueryParameter").HasKey(e => e.ParamId);
            entity.Property(e => e.ParamId).HasColumnName("param_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.IsActive).HasColumnName("active");
        }
    }
}
