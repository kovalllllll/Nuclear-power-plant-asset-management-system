using AssetManagementSystem.DAL.Repositories.Impl;
using AssetManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagementSystem.DAL;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IInspectionRepository, InspectionRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();

        return services;
    }
}