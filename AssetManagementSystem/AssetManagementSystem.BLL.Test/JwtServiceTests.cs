using System.IdentityModel.Tokens.Jwt;
using AssetManagementSystem.BLL.Options;
using AssetManagementSystem.BLL.Services.Impl;
using AssetManagementSystem.DAL.Entities;
using Microsoft.Extensions.Options;
using Moq;

namespace AssetManagementSystem.BLL.Test;

public class JwtServiceTests
{
    private readonly JwtService _jwtService;
    private readonly JwtOptions _jwtOptions;

    public JwtServiceTests()
    {
        _jwtOptions = new JwtOptions
        {
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            Key = "ThisIsASecretKeyForTestingPurposes123!"
        };

        var optionsMock = new Mock<IOptions<JwtOptions>>();
        optionsMock.Setup(x => x.Value).Returns(_jwtOptions);

        _jwtService = new JwtService(optionsMock.Object);
    }

    [Fact]
    public async Task GenerateJwtTokenAsync_ReturnsValidToken()
    {
        var user = new User { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@test.com" };

        var token = await _jwtService.GenerateJwtTokenAsync(user);

        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public async Task GenerateJwtTokenAsync_TokenContainsCorrectClaims()
    {
        var user = new User { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@test.com" };

        var token = await _jwtService.GenerateJwtTokenAsync(user);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        Assert.Equal(_jwtOptions.Issuer, jwtToken.Issuer);
        Assert.Contains(jwtToken.Audiences, a => a == _jwtOptions.Audience);
        Assert.Equal(user.Email, jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value);
    }
}