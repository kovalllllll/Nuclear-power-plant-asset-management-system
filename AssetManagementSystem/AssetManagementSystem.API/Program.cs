using AssetManagementSystem.DAL;

namespace AssetManagementSystem.API;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true,
            reloadOnChange: true);

        builder.Services.AddAuthorization();

        builder.Services.AddOpenApi();

        builder.Services.AddDataAccessLayer();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}