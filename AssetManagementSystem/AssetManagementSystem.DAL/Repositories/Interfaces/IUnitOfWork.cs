namespace AssetManagementSystem.DAL.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAssetRepository Assets { get; }
    IInspectionRepository Inspections { get; }
    ITransferRepository Transfers { get; }
    IReportRepository Reports { get; }

    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}