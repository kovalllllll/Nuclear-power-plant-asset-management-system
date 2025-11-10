using AssetManagementSystem.DAL.Enums;

namespace AssetManagementSystem.DAL.Entities;

public class Inspection
{
    public Guid Id { get; set; }
    public Guid AssetId { get; set; }
    public Guid InspectorId { get; set; }
    public DateTime InspectionDate { get; set; }
    public AssetStatus StatusBefore { get; set; }
    public AssetStatus StatusAfter { get; set; }
    public string LocationBefore { get; set; }
    public string LocationAfter { get; set; }
    public string Notes { get; set; }
    public string PhotoPath { get; set; }
    public DateTime CreatedAt { get; set; }
}
