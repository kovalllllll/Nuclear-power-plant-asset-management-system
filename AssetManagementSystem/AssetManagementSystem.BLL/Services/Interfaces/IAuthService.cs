using AssetManagementSystem.BLL.Models;

namespace AssetManagementSystem.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<AuthUserModel> SignUpAsync(SignUpModel model);
    Task<AuthUserModel> SignInAsync(SignInModel model);
}