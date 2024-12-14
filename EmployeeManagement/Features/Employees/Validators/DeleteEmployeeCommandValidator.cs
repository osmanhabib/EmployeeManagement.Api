using EmployeeManagement.Features.Employees.Commands;
using FluentValidation;

namespace EmployeeManagement.Features.Employees.Validators;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotNull()
            .NotEmpty()
            .WithMessage("EmployeeId must not be empty.");
    }
}
