using AssetManagementSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManagementSystem.DAL.EntityConfigurations;

public class InspectionEntityConfiguration : IEntityTypeConfiguration<Inspection>

{
    public void Configure(EntityTypeBuilder<Inspection> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("Inspections");

        builder.Property(e => e.AssetId);

        builder.Property(e => e.InspectorId);

        builder.Property(e => e.InspectionDate);

        builder.Property(e => e.StatusBefore);

        builder.Property(e => e.StatusAfter);
        
        builder.Property(e => e.LocationBefore)
            .HasMaxLength(200);
        
        builder.Property(e => e.LocationAfter)
            .HasMaxLength(200);
        
        builder.Property(e => e.Notes)
            .HasMaxLength(2000);
        
        builder.Property(e => e.PhotoPath)
            .HasMaxLength(500);
        
        builder.Property(e => e.CreatedAt);
        
        builder.HasOne<Asset>()
            .WithMany()
            .HasForeignKey(e => e.AssetId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.InspectorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}