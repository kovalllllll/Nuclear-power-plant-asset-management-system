using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Enums;

namespace AssetManagementSystem.DAL.Repositories.Interfaces;

public interface ITransferRepository : IRepository<Transfer, Guid>
{
    IEnumerable<Transfer> GetByAssetId(Guid assetId);
    IEnumerable<Transfer> GetByStatus(TransferStatus status);
    IEnumerable<Transfer> GetPendingTransfers();
}