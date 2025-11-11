using AssetManagementSystem.DAL.Repositories.Impl;
using AssetManagementSystem.DAL.Repositories.Interfaces;

namespace AssetManagementSystem.DAL;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private IAssetRepository? _assetRepository;
    private IInspectionRepository? _inspectionRepository;
    private IReportRepository? _reportRepository;
    private ITransferRepository? _transferRepository;


    public IAssetRepository Assets => _assetRepository ??= new AssetRepository(context);
    public IInspectionRepository Inspections => _inspectionRepository ??= new InspectionRepository(context);
    public ITransferRepository Transfers => _transferRepository ??= new TransferRepository(context);
    public IReportRepository Reports => _reportRepository ??= new ReportRepository(context);


    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}