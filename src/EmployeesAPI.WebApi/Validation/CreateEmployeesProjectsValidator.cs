using FluentValidation;
using EmployeesAPI.Common.Models.Request;

namespace EmployeesAPI.WebApi.Validation;

public class CreateEmployeesProjectsValidator : AbstractValidator<CreateEmployeeProject>
{
    public CreateEmployeesProjectsValidator()
    {
        RuleFor(x => x.DateFrom).NotNull().NotEmpty().WithMessage(ValidationErrors.DATE_FROM_FIELD_REQUIRED);
        RuleFor(x=>x.DateTo).NotNull().NotEmpty().WithMessage(ValidationErrors.DATE_TO_FIELD_REQUIRED);
        RuleFor(x => x.EmpID).NotNull().NotEmpty().WithMessage(ValidationErrors.EMPID_FIELD_REQUIRED);
        RuleFor(x => x.ProjectID).NotNull().NotEmpty().WithMessage(ValidationErrors.PROJID_FIELD_REQUIRED);

    }
}