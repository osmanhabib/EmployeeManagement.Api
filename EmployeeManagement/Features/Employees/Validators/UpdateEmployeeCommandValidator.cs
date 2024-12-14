using EmployeeManagement.Features.Employees.Commands;
using FluentValidation;

namespace EmployeeManagement.Features.Employees.Validators;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotNull()
            .NotEmpty()
            .WithMessage("EmployeeId must not be empty.");
    }
}
