using EmployeesAPI.Persistence.Entities;

namespace EmployeesAPI.Persistence;

public interface IUnitOfWork
{
    IRepository<EmployeeProjects> EmployeeProjects { get; }

    void Commit();
    void Rollback();
}
