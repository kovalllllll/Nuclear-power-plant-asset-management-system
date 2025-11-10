namespace AssetManagementSystem.DAL.Repositories.Interfaces;

public interface IRepository<T, in TId> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(TId id);
    IEnumerable<T> Find(Func<T, bool> predicate, int pageNumber = 0, int pageSize = 10);
    T Create(T entity);
    T Update(T entity);
    void Delete(T entity);
    void Delete(TId id);
    void SaveChanges();
}