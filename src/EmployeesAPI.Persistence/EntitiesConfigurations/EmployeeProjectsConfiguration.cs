using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeesAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Omnia.AlarmsService.Persistence.EntitiesDbConfigurations;

public class EmployeeProjectsConfiguration : IEntityTypeConfiguration<EmployeeProjects>
{
    public void Configure(EntityTypeBuilder<EmployeeProjects> builder)
    {
        
    }
}
