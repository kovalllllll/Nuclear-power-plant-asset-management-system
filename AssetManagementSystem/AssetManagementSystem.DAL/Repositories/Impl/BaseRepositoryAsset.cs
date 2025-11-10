using AssetManagementSystem.DAL.Excetions;
using AssetManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.DAL.Repositories.Impl;

public abstract class BaseRepositoryAsset<T, TId>(ApplicationDbContext dbContext) : IRepository<T, TId>
    where T : class
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(TId id)
    {
        var entity = _dbSet.Find(id);
        return entity ?? throw new NotFoundException($"{typeof(T).Name} with id {id} not found");
    }


    public IEnumerable<T> Find(Func<T, bool> predicate, int pageNumber = 0, int pageSize = 10)
    {
        return _dbSet.Where(predicate)
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToList();
    }

    public T Create(T entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public T Update(T entity)
    {
        return _dbSet.Update(entity).Entity;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Delete(TId id)
    {
        var entity = GetById(id);
        _dbSet.Remove(entity);
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}