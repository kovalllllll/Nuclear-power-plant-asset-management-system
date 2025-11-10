using AssetManagementSystem.DAL.Enums;

namespace AssetManagementSystem.DAL.Entities;

public class Transfer
{
    public Guid Id { get; set; }
    public Guid AssetId { get; set; }
    public string FromLocation { get; set; }
    public string ToLocation { get; set; }
    public DateTime TransferDate { get; set; }
    public string TransferReason { get; set; }
    public TransferStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
