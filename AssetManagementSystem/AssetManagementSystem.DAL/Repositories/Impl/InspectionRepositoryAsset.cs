using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Repositories.Interfaces;

namespace AssetManagementSystem.DAL.Repositories.Impl;

public class InspectionRepositoryAsset(ApplicationDbContext dbContext)
    : BaseRepositoryAsset<Inspection, Guid>(dbContext), IInspectionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public IEnumerable<Inspection> GetByAssetId(Guid assetId)
    {
        return _dbContext.Inspections.Where(i => i.AssetId == assetId).ToList();
    }

    public IEnumerable<Inspection> GetByInspectorId(Guid inspectorId)
    {
        return _dbContext.Inspections.Where(i => i.InspectorId == inspectorId).ToList();
    }
}