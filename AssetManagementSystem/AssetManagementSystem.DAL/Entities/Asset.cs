using AssetManagementSystem.DAL.Enums;

namespace AssetManagementSystem.DAL.Entities;

public class Asset
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }
    public AssetStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}