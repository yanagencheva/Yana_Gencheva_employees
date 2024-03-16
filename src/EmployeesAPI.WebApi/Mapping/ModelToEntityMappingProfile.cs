using AutoMapper;
using EmployeesAPI.Common.Models.Request;
using EmployeesAPI.Persistence.Entities;

namespace Omnia.AlarmsService.WebApi.Mapping;

public class ModelToEntityMappingProfile : Profile
{
    public ModelToEntityMappingProfile()
    {
        CreateMap<CreateEmployeeProject, EmployeeProjects>();
    }
}
