using AssetManagementSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManagementSystem.DAL.EntityConfigurations;

public class TransferEntityConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("Transfers");

        builder.Property(e => e.AssetId);

        builder.Property(e => e.FromLocation)
            .HasMaxLength(200);

        builder.Property(e => e.ToLocation)
            .HasMaxLength(200);

        builder.Property(e => e.TransferDate);

        builder.Property(e => e.TransferReason)
            .HasMaxLength(500);

        builder.Property(e => e.Status);

        builder.Property(e => e.CreatedAt);

        builder.HasOne<Asset>()
            .WithMany()
            .HasForeignKey(e => e.AssetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}