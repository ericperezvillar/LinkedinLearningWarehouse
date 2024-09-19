using LinkedinLearningWarehouse.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LinkedinLearningWarehouse.Configuration
{
    internal class IntegratorHistoryConfiguration : IEntityTypeConfiguration<IntegratorHistory>
    {
        public void Configure(EntityTypeBuilder<IntegratorHistory> entity)
        {
            entity.ToTable("IntegratorHistory", schema: "processor").HasKey(o => o.Id);

            entity.Property(o => o.DateOfProcessing).HasColumnName("date_of_process");
        }
    }
}
