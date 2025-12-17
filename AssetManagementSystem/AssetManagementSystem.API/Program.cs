using AssetManagementSystem.BLL;
using AssetManagementSystem.BLL.Exceptions;
using AssetManagementSystem.BLL.Models;
using AssetManagementSystem.BLL.Services.Interfaces;
using AssetManagementSystem.DAL;
using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.API;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true,
            reloadOnChange: true);

        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthorization();

        builder.Services.AddOpenApi();

        builder.Services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddDataAccessLayer(builder.Configuration)
            .AddBusinessLogicLayer()
            .AddAuth(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Map("/", () => "Asset Management System API is running.");

        var auth = app.MapGroup("/api/auth").WithTags("Auth");

        auth.MapPost("/sign-up", async (SignUpModel model, IAuthService authService) =>
        {
            try
            {
                var result = await authService.SignUpAsync(model);
                return Results.Ok(result);
            }
            catch (BadRequestException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (ValidationException ex)
            {
                return Results.UnprocessableEntity(new { error = ex.Message });
            }
        });

        auth.MapPost("/sign-in", async (SignInModel model, IAuthService authService) =>
        {
            try
            {
                var result = await authService.SignInAsync(model);
                return Results.Ok(result);
            }
            catch (InvalidEmailOrPasswordException)
            {
                return Results.Unauthorized();
            }
        });


        app.Run();
    }
}