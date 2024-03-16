using AutoMapper;
using EmployeesAPI.Common.Models.Response;
using EmployeesAPI.Persistence.Entities;

namespace Omnia.AlarmsService.WebApi.Mapping;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<EmployeeProjects, EmployeesProjectsResponse>();
    }
}
