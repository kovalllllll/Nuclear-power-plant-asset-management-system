using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.DAL.Tests;

public static class InMemoryDbFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}