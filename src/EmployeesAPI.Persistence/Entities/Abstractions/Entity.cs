using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Persistence.Entities.Abstractions;

public abstract class Entity
{
    [Key]
    ///public int Id { get; set; }
    public Guid Id { get; set; }
}
