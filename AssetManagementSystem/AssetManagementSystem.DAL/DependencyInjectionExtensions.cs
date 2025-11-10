using AssetManagementSystem.DAL.Repositories.Impl;
using AssetManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagementSystem.DAL;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddScoped<IAssetRepository, AssetRepositoryAsset>();
        services.AddScoped<IInspectionRepository, InspectionRepositoryAsset>();
        services.AddScoped<ITransferRepository, TransferRepositoryAsset>();
        services.AddScoped<IReportRepository, ReportRepositoryAsset>();
        services.AddScoped<IReportRepository, ReportRepositoryAsset>();

        return services;
    }
}