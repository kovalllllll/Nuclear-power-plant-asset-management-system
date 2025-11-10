using Microsoft.EntityFrameworkCore;
using Moq;

namespace AssetManagementSystem.DAL.Tests;

public class UnitOfWorkTests
{
    private static ApplicationDbContext MakeContextMock(out Mock<ApplicationDbContext> ctxMock)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        ctxMock = new Mock<ApplicationDbContext>(options) { CallBase = true };
        return ctxMock.Object;
    }

    [Fact]
    public void Repositories_ShouldBeCachedPerUoW()
    {
        var ctx = MakeContextMock(out _);
        var uow = new UnitOfWork(ctx);

        var r1 = uow.Assets;
        var r2 = uow.Assets;

        Assert.Same(r1, r2);
    }

    [Fact]
    public void SaveChanges_ShouldCallContext()
    {
        var ctx = MakeContextMock(out var mock);
        var uow = new UnitOfWork(ctx);

        uow.SaveChanges();

        mock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldCallContextAsync()
    {
        var ctx = MakeContextMock(out var mock);
        var uow = new UnitOfWork(ctx);

        await uow.SaveChangesAsync();

        mock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}