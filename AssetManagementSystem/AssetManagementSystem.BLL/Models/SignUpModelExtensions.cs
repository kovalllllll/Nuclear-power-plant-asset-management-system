using AssetManagementSystem.DAL.Entities;

namespace AssetManagementSystem.BLL.Models;

public static class SignUpModelExtensions
{
    public static User ToEntity(this SignUpModel model) => new()
    {
        UserName = model.Email,
        Email = model.Email
    };
}