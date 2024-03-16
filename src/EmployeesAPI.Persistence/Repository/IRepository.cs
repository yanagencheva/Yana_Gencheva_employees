
namespace EmployeesAPI.Persistence;

public interface IRepository<T> where T : class
{
    T Add(T entity);
    void Delete(T entity);
    void Update(T entity);
    void AddMany(IEnumerable<T> entities);
    void UpdateMany(IEnumerable<T> entities);
    void DeleteMany(IEnumerable<T> entities);
    T Get(Guid id, bool tracked = false);
    IQueryable<T> All(bool tracked = false);
}
