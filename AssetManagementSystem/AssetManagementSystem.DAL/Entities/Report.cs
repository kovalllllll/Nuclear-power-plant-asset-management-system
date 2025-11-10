using AssetManagementSystem.DAL.Enums;

namespace AssetManagementSystem.DAL.Entities;

public class Report
{
    public Guid Id { get; set; }
    public string ReportName { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string FilePath { get; set; }
    public ReportStatus Status { get; set; }
}