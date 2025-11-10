using AssetManagementSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManagementSystem.DAL.EntityConfigurations;

public class ReportEntityConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("Reports");

        builder.Property(e => e.ReportName)
            .HasMaxLength(200);

        builder.Property(e => e.GeneratedAt);

        builder.Property(e => e.FilePath)
            .HasMaxLength(500);

        builder.Property(e => e.Status);
    }
}