using AssetManagementSystem.BLL.Exceptions;
using AssetManagementSystem.BLL.Models;
using AssetManagementSystem.BLL.Services.Impl;
using AssetManagementSystem.BLL.Services.Interfaces;
using AssetManagementSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AssetManagementSystem.BLL.Test;

public class AuthServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _jwtServiceMock = new Mock<IJwtService>();
        _authService = new AuthService(_userManagerMock.Object, _jwtServiceMock.Object);
    }

    [Fact]
    public async Task SignUpAsync_WhenUserExists_ThrowsBadRequestException()
    {
        var model = new SignUpModel { Email = "test@test.com", Password = "Password123!" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(model.Email))
            .ReturnsAsync(new User());

        await Assert.ThrowsAsync<BadRequestException>(() => _authService.SignUpAsync(model));
    }

    [Fact]
    public async Task SignUpAsync_WhenCreateFails_ThrowsValidationException()
    {
        var model = new SignUpModel { Email = "test@test.com", Password = "weak" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), model.Password))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password too weak" }));

        await Assert.ThrowsAsync<ValidationException>(() => _authService.SignUpAsync(model));
    }

    [Fact]
    public async Task SignUpAsync_WhenSuccess_ReturnsAuthUserModel()
    {
        var model = new SignUpModel { Email = "test@test.com", Password = "Password123!" };
        var expectedToken = "jwt-token";

        _userManagerMock.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), model.Password))
            .ReturnsAsync(IdentityResult.Success);
        _jwtServiceMock.Setup(x => x.GenerateJwtTokenAsync(It.IsAny<User>()))
            .ReturnsAsync(expectedToken);

        var result = await _authService.SignUpAsync(model);

        Assert.Equal(expectedToken, result.AccessToken);
    }

    [Fact]
    public async Task SignInAsync_WhenUserNotFound_ThrowsInvalidEmailOrPasswordException()
    {
        var model = new SignInModel { Email = "test@test.com", Password = "Password123!" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);

        await Assert.ThrowsAsync<InvalidEmailOrPasswordException>(() => _authService.SignInAsync(model));
    }

    [Fact]
    public async Task SignInAsync_WhenPasswordInvalid_ThrowsInvalidEmailOrPasswordException()
    {
        var model = new SignInModel { Email = "test@test.com", Password = "wrong" };
        var user = new User { Email = model.Email };

        _userManagerMock.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, model.Password)).ReturnsAsync(false);

        await Assert.ThrowsAsync<InvalidEmailOrPasswordException>(() => _authService.SignInAsync(model));
    }

    [Fact]
    public async Task SignInAsync_WhenSuccess_ReturnsAuthUserModel()
    {
        var model = new SignInModel { Email = "test@test.com", Password = "Password123!" };
        var user = new User { Email = model.Email };
        var expectedToken = "jwt-token";

        _userManagerMock.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, model.Password)).ReturnsAsync(true);
        _jwtServiceMock.Setup(x => x.GenerateJwtTokenAsync(user)).ReturnsAsync(expectedToken);

        var result = await _authService.SignInAsync(model);

        Assert.Equal(expectedToken, result.AccessToken);
    }
}