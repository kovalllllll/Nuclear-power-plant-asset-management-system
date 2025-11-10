using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Enums;

namespace AssetManagementSystem.DAL.Repositories.Interfaces;

public interface IReportRepository : IRepository<Report, Guid>
{
    IEnumerable<Report> GetByStatus(ReportStatus status);
}