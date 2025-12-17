using AssetManagementSystem.BLL.Options;
using AssetManagementSystem.BLL.Services.Impl;
using AssetManagementSystem.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagementSystem.BLL;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}