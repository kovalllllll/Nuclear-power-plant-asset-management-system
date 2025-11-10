using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Enums;

namespace AssetManagementSystem.DAL.Repositories.Interfaces;

public interface IAssetRepository : IRepository<Asset, Guid>
{
    IEnumerable<Asset> GetByStatus(AssetStatus status);
    IEnumerable<Asset> GetByCategory(string category);
}