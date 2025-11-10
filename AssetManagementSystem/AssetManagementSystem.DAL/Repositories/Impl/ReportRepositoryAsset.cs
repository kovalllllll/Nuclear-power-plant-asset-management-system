using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Enums;
using AssetManagementSystem.DAL.Repositories.Interfaces;

namespace AssetManagementSystem.DAL.Repositories.Impl;

public class ReportRepositoryAsset(ApplicationDbContext dbContext)
    : BaseRepositoryAsset<Report, Guid>(dbContext), IReportRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public IEnumerable<Report> GetByStatus(ReportStatus status)
    {
        return _dbContext.Reports.Where(r => r.Status == status).ToList();
    }
}