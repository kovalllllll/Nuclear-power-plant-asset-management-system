using AssetManagementSystem.BLL.Exceptions;
using AssetManagementSystem.BLL.Models;
using AssetManagementSystem.BLL.Services.Interfaces;
using AssetManagementSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.BLL.Services.Impl;

public class AuthService(UserManager<User> userManager, IJwtService jwtService) : IAuthService
{
    public async Task<AuthUserModel> SignUpAsync(SignUpModel model)
    {
        var existingUser = await userManager.FindByEmailAsync(model.Email);
        if (existingUser is not null)
        {
            throw new BadRequestException("User with this email already exists.");
        }

        var user = model.ToEntity();
        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new ValidationException(errors);
        }

        var token = await jwtService.GenerateJwtTokenAsync(user);

        return new AuthUserModel { AccessToken = token };
    }

    public async Task<AuthUserModel> SignInAsync(SignInModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            throw new InvalidEmailOrPasswordException("Invalid email or password.");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);
        if (!isPasswordValid)
        {
            throw new InvalidEmailOrPasswordException("Invalid email or password.");
        }

        var token = await jwtService.GenerateJwtTokenAsync(user);

        return new AuthUserModel { AccessToken = token };
    }
}