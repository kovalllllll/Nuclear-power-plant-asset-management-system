using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Enums;
using AssetManagementSystem.DAL.Excetions;
using AssetManagementSystem.DAL.Repositories.Impl;

namespace AssetManagementSystem.DAL.Tests.Tests;

public class AssetRepositoryTests
{
    [Fact]
    public void Create_ShouldAddEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var repo = new AssetRepository(ctx);
        var id = Guid.NewGuid();
        var asset = new Asset
            { Id = id, Name = "Gen", Category = "Equip", Location = "A-101", Status = AssetStatus.Working };

        repo.Create(asset);
        repo.SaveChanges();

        var saved = ctx.Assets.Find(id);
        Assert.NotNull(saved);
        Assert.Equal("Gen", saved.Name);
    }

    [Fact]
    public void GetAll_ShouldReturnAll()
    {
        using var ctx = InMemoryDbFactory.Create();
        ctx.Assets.AddRange(
            new Asset
            {
                Id = Guid.NewGuid(), Name = "Gen1", Category = "Equip1", Location = "A-101",
                Status = AssetStatus.Working
            }
        );
        ctx.SaveChanges();

        var repo = new AssetRepository(ctx);
        var all = repo.GetAll();

        Assert.Single(all);
    }

    [Fact]
    public void GetById_ShouldReturn_WhenExists()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        ctx.Assets.Add(new Asset
            { Id = id, Name = "A1", Category = "Equip", Location = "A-101", Status = AssetStatus.Working });
        ctx.SaveChanges();

        var repo = new AssetRepository(ctx);
        var entity = repo.GetById(id);

        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void GetById_ShouldThrow_WhenNotFound()
    {
        using var ctx = InMemoryDbFactory.Create();
        var repo = new AssetRepository(ctx);

        Assert.Throws<NotFoundException>(() => repo.GetById(Guid.NewGuid()));
    }

    [Fact]
    public void Update_ShouldModifyEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        ctx.Assets.Add(new Asset
            { Id = id, Name = "Old", Category = "Equip", Location = "A-101", Status = AssetStatus.Working });
        ctx.SaveChanges();

        var repo = new AssetRepository(ctx);
        var entity = repo.GetById(id);
        entity.Name = "New";
        repo.Update(entity);
        repo.SaveChanges();

        Assert.Equal("New", ctx.Assets.Find(id)!.Name);
    }

    [Fact]
    public void Delete_ById_ShouldRemoveEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        ctx.Assets.Add(new Asset
            { Id = id, Name = "Gen", Category = "Equip", Location = "A-101", Status = AssetStatus.Working });
        ctx.SaveChanges();

        var repo = new AssetRepository(ctx);
        repo.Delete(id);
        repo.SaveChanges();

        Assert.Null(ctx.Assets.Find(id));
    }

    [Fact]
    public void Find_ShouldFilterAndPage()
    {
        using var ctx = InMemoryDbFactory.Create();
        ctx.Assets.AddRange(
            new Asset
            {
                Id = Guid.NewGuid(), Name = "A1", Category = "Equip1", Location = "A-101", Status = AssetStatus.Working
            },
            new Asset
            {
                Id = Guid.NewGuid(), Name = "A2", Category = "Equip1", Location = "A-101", Status = AssetStatus.Working
            },
            new Asset
            {
                Id = Guid.NewGuid(), Name = "A3", Category = "Equip1", Location = "A-101", Status = AssetStatus.Working
            }
        );
        ctx.SaveChanges();

        var repo = new AssetRepository(ctx);
        var page = repo.Find(a => a.Name.StartsWith("A"), pageNumber: 1, pageSize: 1).ToList();

        Assert.Single(page);
        Assert.Equal("A2", page[0].Name);
    }

    [Fact]
    public void GetByStatus_ShouldReturnFilteredAssets()
    {
        using var ctx = InMemoryDbFactory.Create();
        ctx.Assets.AddRange(
            new Asset
            {
                Id = Guid.NewGuid(), Name = "Asset1", Category = "Category1", Location = "Location1",
                Status = AssetStatus.Working
            },
            new Asset
            {
                Id = Guid.NewGuid(), Name = "Asset2", Category = "Category2", Location = "Location2",
                Status = AssetStatus.Broken
            }
        );
        ctx.SaveChanges();

        var repo = new AssetRepository(ctx);
        var workingAssets = repo.GetByStatus(AssetStatus.Working).ToList();

        Assert.Single(workingAssets);
        Assert.Equal("Asset1", workingAssets[0].Name);
    }

    [Fact]
    public void GetByCategory_ShouldReturnFilteredAssets()
    {
        using var ctx = InMemoryDbFactory.Create();
        ctx.Assets.AddRange(
            new Asset
            {
                Id = Guid.NewGuid(), Name = "Asset1", Category = "Category1", Location = "Location1",
                Status = AssetStatus.Working
            },
            new Asset
            {
                Id = Guid.NewGuid(), Name = "Asset2", Category = "Category2", Location = "Location2",
                Status = AssetStatus.Broken
            }
        );
        ctx.SaveChanges();

        var repo = new AssetRepository(ctx);
        var category1Assets = repo.GetByCategory("Category1").ToList();

        Assert.Single(category1Assets);
        Assert.Equal("Asset1", category1Assets[0].Name);
    }
}