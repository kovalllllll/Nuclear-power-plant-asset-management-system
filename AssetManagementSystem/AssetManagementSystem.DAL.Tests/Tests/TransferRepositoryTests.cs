using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Repositories.Impl;

namespace AssetManagementSystem.DAL.Tests.Tests;

public class TransferRepositoryTests
{
    [Fact]
    public void Create_ShouldAddEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var repo = new TransferRepository(ctx);
        var id = Guid.NewGuid();
        var transfer = new Transfer
        {
            Id = id, AssetId = Guid.NewGuid(), FromLocation = "A-101", ToLocation = "B-202",
            TransferDate = DateTime.UtcNow, TransferReason = "Routine Move"
        };

        repo.Create(transfer);
        repo.SaveChanges();
        var saved = ctx.Transfers.Find(id);
        Assert.NotNull(saved);
        Assert.Equal("A-101", saved.FromLocation);
    }

    [Fact]
    public void GetAll_ShouldReturnAll()
    {
        using var ctx = InMemoryDbFactory.Create();
        ctx.Transfers.AddRange(
            new Transfer
            {
                Id = Guid.NewGuid(), AssetId = Guid.NewGuid(), FromLocation = "A-101", ToLocation = "B-202",
                TransferDate = DateTime.UtcNow, TransferReason = "Routine Move"
            }
        );
        ctx.SaveChanges();
        var repo = new TransferRepository(ctx);
        var all = repo.GetAll();
        Assert.Single(all);
    }

    [Fact]
    public void GetById_ShouldReturn_WhenExists()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        ctx.Transfers.Add(new Transfer
        {
            Id = id, AssetId = Guid.NewGuid(), FromLocation = "A-101", ToLocation = "B-202",
            TransferDate = DateTime.UtcNow, TransferReason = "Routine Move"
        });
        ctx.SaveChanges();
        var repo = new TransferRepository(ctx);
        var entity = repo.GetById(id);
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void Delete_ShouldRemoveEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        var transfer = new Transfer
        {
            Id = id, AssetId = Guid.NewGuid(), FromLocation = "A-101", ToLocation = "B-202",
            TransferDate = DateTime.UtcNow, TransferReason = "Routine Move"
        };
        ctx.Transfers.Add(transfer);
        ctx.SaveChanges();

        var repo = new TransferRepository(ctx);
        repo.Delete(id);
        repo.SaveChanges();

        var deleted = ctx.Transfers.Find(id);
        Assert.Null(deleted);
    }

    [Fact]
    public void Update_ShouldModifyEntity()
    {
        using var ctx = InMemoryDbFactory.Create();
        var id = Guid.NewGuid();
        var transfer = new Transfer
        {
            Id = id, AssetId = Guid.NewGuid(), FromLocation = "A-101", ToLocation = "B-202",
            TransferDate = DateTime.UtcNow, TransferReason = "Routine Move"
        };
        ctx.Transfers.Add(transfer);
        ctx.SaveChanges();

        var repo = new TransferRepository(ctx);
        transfer.ToLocation = "C-303";
        repo.Update(transfer);
        repo.SaveChanges();
    }

    [Fact]
    public void Find_ShouldReturnFilteredResults()
    {
        using var ctx = InMemoryDbFactory.Create();
        ctx.Transfers.AddRange(
            new Transfer
            {
                Id = Guid.NewGuid(), AssetId = Guid.NewGuid(), FromLocation = "A-101", ToLocation = "B-202",
                TransferDate = DateTime.UtcNow, TransferReason = "Routine Move"
            },
            new Transfer
            {
                Id = Guid.NewGuid(), AssetId = Guid.NewGuid(), FromLocation = "C-303", ToLocation = "D-404",
                TransferDate = DateTime.UtcNow, TransferReason = "Relocation"
            }
        );
        ctx.SaveChanges();

        var repo = new TransferRepository(ctx);
        var results = repo.Find(t => t.FromLocation.StartsWith("A"));
        Assert.Single(results);
    }
}