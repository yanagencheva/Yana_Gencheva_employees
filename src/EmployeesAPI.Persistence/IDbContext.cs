using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EmployeesAPI.Persistence;

public interface IDbContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    EntityEntry<T> Entry<T>(T Entity) where T : class;
    DatabaseFacade Database { get; }
    int SaveChanges();
}
