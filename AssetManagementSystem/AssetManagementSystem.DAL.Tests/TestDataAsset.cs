using AssetManagementSystem.DAL.Entities;
using AssetManagementSystem.DAL.Enums;

namespace AssetManagementSystem.DAL.Tests;

public class TestDataAsset
{
    public static Asset NewAsset(
        Guid? id = null,
        string name = "Gen",
        string category = "Equip",
        string location = "A-101",
        AssetStatus status = AssetStatus.Working)
    {
        return new Asset
        {
            Id = id ?? Guid.NewGuid(),
            Name = name,
            Category = category,
            Location = location,
            Status = status
        };
    }
}