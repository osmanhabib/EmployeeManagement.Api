using EmployeeManagement.Features.Employees.Queries;
using FluentValidation;

namespace EmployeeManagement.Features.Employees.Validators;

public class GetEmployeeQueryValidator : AbstractValidator<GetEmployeeQuery>
{
    public GetEmployeeQueryValidator()
    {
        RuleFor(x => x.EmployeeId)
           .NotNull()
           .NotEmpty()
           .WithMessage("EmployeeId must not be empty.");
    }
}
