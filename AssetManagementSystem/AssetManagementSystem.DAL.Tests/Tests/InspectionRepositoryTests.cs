using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Excetions;
using AssetManagementSystem.DAL.Repositories.Impl;

namespace AssetManagementSystem.DAL.Tests.Tests;

public class InspectionRepositoryTests
{
    [Fact]
    public void Create_ShouldAddEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var repo = new InspectionRepository(ctx);
        var id = Guid.NewGuid();
        var inspection = new Inspection
        {
            Id = id,
            AssetId = Guid.NewGuid(),
            InspectorId = Guid.NewGuid(),
            InspectionDate = DateTime.UtcNow,
            StatusBefore = Enums.AssetStatus.Working,
            StatusAfter = Enums.AssetStatus.Decommissioned,
            LocationBefore = "A-101",
            LocationAfter = "Maintenance Room",
            Notes = "Routine check found issues.",
            PhotoPath = "/photos/inspection1.jpg",
            CreatedAt = DateTime.UtcNow
        };

        repo.Create(inspection);
        repo.SaveChanges();
        var saved = ctx.Inspections.Find(id);
        Assert.NotNull(saved);
        Assert.Equal("Routine check found issues.", saved.Notes);
    }

    [Fact]
    public void GetAll_ShouldReturnAll()
    {
        using var ctx = InMemoryDbFactory.Create();
        ctx.Inspections.AddRange(
            new Inspection
            {
                Id = Guid.NewGuid(),
                AssetId = Guid.NewGuid(),
                InspectorId = Guid.NewGuid(),
                InspectionDate = DateTime.UtcNow,
                StatusBefore = Enums.AssetStatus.Working,
                StatusAfter = Enums.AssetStatus.Working,
                LocationBefore = "A-101",
                LocationAfter = "A-101",
                Notes = "All good.",
                PhotoPath = "/photos/inspection2.jpg",
                CreatedAt = DateTime.UtcNow
            });
        ctx.SaveChanges();
        var repo = new InspectionRepository(ctx);
        var all = repo.GetAll();
        Assert.Single(all);
    }

    [Fact]
    public void GetById_ShouldReturn_WhenExists()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        ctx.Inspections.Add(new Inspection
        {
            Id = id,
            AssetId = Guid.NewGuid(),
            InspectorId = Guid.NewGuid(),
            InspectionDate = DateTime.UtcNow,
            StatusBefore = Enums.AssetStatus.Working,
            StatusAfter = Enums.AssetStatus.Working,
            LocationBefore = "A-101",
            LocationAfter = "A-101",
            Notes = "Inspection notes.",
            PhotoPath = "/photos/inspection3.jpg",
            CreatedAt = DateTime.UtcNow
        });
        ctx.SaveChanges();
        var repo = new InspectionRepository(ctx);
        var entity = repo.GetById(id);
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void GetById_ShouldThrow_WhenNotFound()
    {
        using var ctx = InMemoryDbFactory.Create();
        var repo = new InspectionRepository(ctx);
        var nonExistentId = Guid.NewGuid();
        Assert.Throws<NotFoundException>(() => repo.GetById(nonExistentId));
    }

    [Fact]
    public void Update_ShouldModifyEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        ctx.Inspections.Add(new Inspection
        {
            Id = id,
            AssetId = Guid.NewGuid(),
            InspectorId = Guid.NewGuid(),
            InspectionDate = DateTime.UtcNow,
            StatusBefore = Enums.AssetStatus.Working,
            StatusAfter = Enums.AssetStatus.Working,
            LocationBefore = "A-101",
            LocationAfter = "A-101",
            Notes = "Initial notes.",
            PhotoPath = "/photos/inspection4.jpg",
            CreatedAt = DateTime.UtcNow
        });
        ctx.SaveChanges();
        var repo = new InspectionRepository(ctx);
        var entity = repo.GetById(id);
        entity.Notes = "Updated inspection notes.";
        repo.Update(entity);
        repo.SaveChanges();
        var updated = ctx.Inspections.Find(id);
        Assert.Equal("Updated inspection notes.", updated.Notes);
    }

    [Fact]
    public void Delete_ById_ShouldRemoveEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        ctx.Inspections.Add(new Inspection
        {
            Id = id,
            AssetId = Guid.NewGuid(),
            InspectorId = Guid.NewGuid(),
            InspectionDate = DateTime.UtcNow,
            StatusBefore = Enums.AssetStatus.Working,
            StatusAfter = Enums.AssetStatus.Working,
            LocationBefore = "A-101",
            LocationAfter = "A-101",
            Notes = "To be deleted.",
            PhotoPath = "/photos/inspection5.jpg",
            CreatedAt = DateTime.UtcNow
        });
        ctx.SaveChanges();
        var repo = new InspectionRepository(ctx);
        repo.Delete(id);
        repo.SaveChanges();
        Assert.Null(ctx.Inspections.Find(id));
    }

    [Fact]
    public void Find_ShouldFilterAndPage()
    {
        using var ctx = InMemoryDbFactory.Create();
        ctx.Inspections.AddRange(
            new Inspection
            {
                Id = Guid.NewGuid(),
                AssetId = Guid.NewGuid(),
                InspectorId = Guid.NewGuid(),
                InspectionDate = DateTime.UtcNow,
                StatusBefore = Enums.AssetStatus.Working,
                StatusAfter = Enums.AssetStatus.Working,
                LocationBefore = "A-101",
                LocationAfter = "A-101",
                Notes = "Inspection A.",
                PhotoPath = "/photos/inspection6.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new Inspection
            {
                Id = Guid.NewGuid(),
                AssetId = Guid.NewGuid(),
                InspectorId = Guid.NewGuid(),
                InspectionDate = DateTime.UtcNow,
                StatusBefore = Enums.AssetStatus.Working,
                StatusAfter = Enums.AssetStatus.Decommissioned,
                LocationBefore = "A-102",
                LocationAfter = "Maintenance Room",
                Notes = "Inspection B.",
                PhotoPath = "/photos/inspection7.jpg",
                CreatedAt = DateTime.UtcNow
            });
        ctx.SaveChanges();
        var repo = new InspectionRepository(ctx);
        var results = repo.Find(i => i.StatusAfter == AssetManagementSystem.DAL.Enums.AssetStatus.Decommissioned, 0,
            10);
        var inspections = results as Inspection[] ?? results.ToArray();
        Assert.Single(inspections);
        Assert.Equal("Inspection B.", inspections.First().Notes);
    }

    [Fact]
    public void GetByAssetId_ShouldReturnInspections()
    {
        using var ctx = InMemoryDbFactory.Create();
        var assetId = Guid.NewGuid();
        ctx.Inspections.AddRange(
            new Inspection
            {
                Id = Guid.NewGuid(),
                AssetId = assetId,
                InspectorId = Guid.NewGuid(),
                InspectionDate = DateTime.UtcNow,
                StatusBefore = Enums.AssetStatus.Working,
                StatusAfter = Enums.AssetStatus.Working,
                LocationBefore = "A-101",
                LocationAfter = "A-101",
                Notes = "Inspection for asset.",
                PhotoPath = "/photos/inspection8.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new Inspection
            {
                Id = Guid.NewGuid(),
                AssetId = Guid.NewGuid(),
                InspectorId = Guid.NewGuid(),
                InspectionDate = DateTime.UtcNow,
                StatusBefore = Enums.AssetStatus.Working,
                StatusAfter = Enums.AssetStatus.Decommissioned,
                LocationBefore = "A-102",
                LocationAfter = "Maintenance Room",
                Notes = "Inspection for another asset.",
                PhotoPath = "/photos/inspection9.jpg",
                CreatedAt = DateTime.UtcNow
            });
        ctx.SaveChanges();
        var repo = new InspectionRepository(ctx);
        var inspections = repo.GetByAssetId(assetId);
        var inspectionList = inspections as Inspection[] ?? inspections.ToArray();
        Assert.Single(inspectionList);
        Assert.Equal("Inspection for asset.", inspectionList.First().Notes);
    }

    [Fact]
    public void GetByInspectorId_ShouldReturnInspections()
    {
        using var ctx = InMemoryDbFactory.Create();
        var inspectorId = Guid.NewGuid();
        ctx.Inspections.AddRange(
            new Inspection
            {
                Id = Guid.NewGuid(),
                AssetId = Guid.NewGuid(),
                InspectorId = inspectorId,
                InspectionDate = DateTime.UtcNow,
                StatusBefore = Enums.AssetStatus.Working,
                StatusAfter = Enums.AssetStatus.Working,
                LocationBefore = "A-101",
                LocationAfter = "A-101",
                Notes = "Inspection by specific inspector.",
                PhotoPath = "/photos/inspection10.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new Inspection
            {
                Id = Guid.NewGuid(),
                AssetId = Guid.NewGuid(),
                InspectorId = Guid.NewGuid(),
                InspectionDate = DateTime.UtcNow,
                StatusBefore = Enums.AssetStatus.Working,
                StatusAfter = Enums.AssetStatus.Decommissioned,
                LocationBefore = "A-102",
                LocationAfter = "Maintenance Room",
                Notes = "Inspection by another inspector.",
                PhotoPath = "/photos/inspection11.jpg",
                CreatedAt = DateTime.UtcNow
            });
        ctx.SaveChanges();
        var repo = new InspectionRepository(ctx);
        var inspections = repo.GetByInspectorId(inspectorId);
        var inspectionList = inspections as Inspection[] ?? inspections.ToArray();
        Assert.Single(inspectionList);
        Assert.Equal("Inspection by specific inspector.", inspectionList.First().Notes);
    }
}