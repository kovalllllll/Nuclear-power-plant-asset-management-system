using AssetManagementSystem.DAL.Entities;

namespace AssetManagementSystem.DAL.Repositories.Interfaces;

public interface IInspectionRepository : IRepository<Inspection, Guid>
{
    IEnumerable<Inspection> GetByAssetId(Guid assetId);
    IEnumerable<Inspection> GetByInspectorId(Guid inspectorId);
}