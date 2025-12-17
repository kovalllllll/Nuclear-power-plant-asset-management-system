using AssetManagementSystem.DAL.Entities;

namespace AssetManagementSystem.BLL.Services.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(User user);
}