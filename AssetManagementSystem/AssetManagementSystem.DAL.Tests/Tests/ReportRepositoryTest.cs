using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.DAL.Tests.Tests;

public class ReportRepositoryTest
{
    [Fact]
    public void GenerateAssetReport_ShouldReturnReportData()
    {
        using var context = InMemoryDbFactory.Create();
        var repo = new ReportRepository(context);
        var id = Guid.NewGuid();
        var report = new Report
        {
            Id = id,
            ReportName = "Asset Report",
            GeneratedAt = DateTime.UtcNow,
            FilePath = "/reports/asset_report.pdf",
            Status = Enums.ReportStatus.Completed
        };

        repo.Create(report);
        repo.SaveChanges();

        var savedReport = context.Reports.Find(id);
        Assert.NotNull(savedReport);
        Assert.Equal("Asset Report", savedReport.ReportName);
    }

    [Fact]
    public void GetReportById_ShouldReturnReport_WhenExists()
    {
        using var context = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        context.Reports.Add(new Report
        {
            Id = id,
            ReportName = "Inventory Report",
            GeneratedAt = DateTime.UtcNow,
            FilePath = "/reports/inventory_report.pdf",
            Status = Enums.ReportStatus.Completed
        });
        context.SaveChanges();
        var repo = new ReportRepository(context);
        var report = repo.GetById(id);
    }

    [Fact]
    public void GetAllReports_ShouldReturnAllReports()
    {
        using var context = InMemoryDbFactory.Create();
        context.Reports.AddRange(
            new Report
            {
                Id = Guid.NewGuid(),
                ReportName = "Report 1",
                GeneratedAt = DateTime.UtcNow,
                FilePath = "/reports/report1.pdf",
                Status = Enums.ReportStatus.Completed
            },
            new Report
            {
                Id = Guid.NewGuid(),
                ReportName = "Report 2",
                GeneratedAt = DateTime.UtcNow,
                FilePath = "/reports/report2.pdf",
                Status = Enums.ReportStatus.InProgress
            }
        );
        context.SaveChanges();

        var repo = new ReportRepository(context);
        var allReports = repo.GetAll();

        Assert.Equal(2, allReports.Count());
    }

    [Fact]
    public void GetReportsByStatus_ShouldReturnFilteredReports()
    {
        using var context = InMemoryDbFactory.Create();
        context.Reports.AddRange(
            new Report
            {
                Id = Guid.NewGuid(),
                ReportName = "Report A",
                GeneratedAt = DateTime.UtcNow,
                FilePath = "/reports/reportA.pdf",
                Status = Enums.ReportStatus.Completed
            },
            new Report
            {
                Id = Guid.NewGuid(),
                ReportName = "Report B",
                GeneratedAt = DateTime.UtcNow,
                FilePath = "/reports/reportB.pdf",
                Status = Enums.ReportStatus.InProgress
            }
        );
        context.SaveChanges();

        var repo = new ReportRepository(context);
        var completedReports = repo.GetByStatus(Enums.ReportStatus.Completed);

        var collection = completedReports.ToList();
        Assert.Single(collection);
        Assert.Equal("Report A", collection.First().ReportName);
    }
}