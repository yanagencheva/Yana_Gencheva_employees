using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EmployeesAPI.Persistence.Entities.Abstractions;

namespace EmployeesAPI.Persistence;

internal class Repository<T> : IRepository<T> where T : Entity, new()
{
    private readonly IDbContext dbContext;

    public Repository(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public T Add(T entity)
    {
        var created = dbContext.Add(entity);
        return created.Entity;
    }

    public void Update(T entity)
    {
        dbContext.Set<T>().Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        dbContext.Entry(entity).State = EntityState.Deleted;
    }

    public void AddMany(IEnumerable<T> entities)
    {
        dbContext.Set<T>().AddRange(entities);
    }

    public void UpdateMany(IEnumerable<T> entities)
    {
        dbContext.Set<T>().UpdateRange(entities);
    }

    public void DeleteMany(IEnumerable<T> entities)
    {
        dbContext.Set<T>().RemoveRange(entities);
    }

    public virtual bool EntityExists(Expression<Func<T, bool>> filter)
    {
        if (filter == null)
        {
            return false;
        }

        IQueryable<T> query = All(false);
        var exist = query.Any(filter);

        return exist;
    }

    public T Get(Guid id, bool tracked = false)
    {
        IQueryable<T> query = All(tracked: tracked);

        var entity = query.SingleOrDefault(x => x.Id == id);
        return entity;
    }

    public IQueryable<T> All(bool tracked = false)
    {
        IQueryable<T> entities = dbContext.Set<T>();
        if (!tracked)
        {
            entities = entities.AsNoTracking();
        }

        return entities;
    }
}