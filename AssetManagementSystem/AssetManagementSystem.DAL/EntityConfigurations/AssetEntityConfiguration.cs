using AssetManagementSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManagementSystem.DAL.EntityConfigurations;

public class AssetEntityConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("Assets");

        builder.Property(e => e.Name)
            .HasMaxLength(200);

        builder.Property(e => e.Category)
            .HasMaxLength(100);

        builder.Property(e => e.Location)
            .HasMaxLength(200);

        builder.Property(e => e.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.Status);

        builder.Property(e => e.CreatedAt);

        builder.Property(e => e.UpdatedAt);
    }
}