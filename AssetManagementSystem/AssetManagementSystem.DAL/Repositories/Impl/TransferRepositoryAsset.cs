using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Enums;
using AssetManagementSystem.DAL.Repositories.Interfaces;

namespace AssetManagementSystem.DAL.Repositories.Impl;

public class TransferRepositoryAsset(ApplicationDbContext dbContext)
    : BaseRepositoryAsset<Transfer, Guid>(dbContext), ITransferRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public IEnumerable<Transfer> GetByAssetId(Guid assetId)
    {
        return _dbContext.Transfers.Where(t => t.AssetId == assetId).ToList();
    }

    public IEnumerable<Transfer> GetByStatus(TransferStatus status)
    {
        return _dbContext.Transfers.Where(t => t.Status == status).ToList();
    }

    public IEnumerable<Transfer> GetPendingTransfers()
    {
        return _dbContext.Transfers.Where(t => t.Status == TransferStatus.Pending).ToList();
    }
}