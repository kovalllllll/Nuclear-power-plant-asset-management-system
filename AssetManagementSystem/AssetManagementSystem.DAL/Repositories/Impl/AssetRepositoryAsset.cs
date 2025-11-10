using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Enums;
using AssetManagementSystem.DAL.Repositories.Interfaces;

namespace AssetManagementSystem.DAL.Repositories.Impl;

public class AssetRepositoryAsset(ApplicationDbContext dbContext) : BaseRepositoryAsset<Asset, Guid>(dbContext), IAssetRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public IEnumerable<Asset> GetByStatus(AssetStatus status)
    {
        return _dbContext.Assets.Where(a => a.Status == status).ToList();
    }

    public IEnumerable<Asset> GetByCategory(string category)
    {
        return _dbContext.Assets.Where(a => a.Category == category).ToList();
    }
}