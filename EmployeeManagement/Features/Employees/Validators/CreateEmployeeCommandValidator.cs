using EmployeeManagement.Features.Employees.Commands;
using FluentValidation;

namespace EmployeeManagement.Features.Employees.Validators;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x=>x.FirstName).NotNull()
            .NotEmpty()
            .WithMessage("First name is required.");

        RuleFor(x=>x.LastName).NotNull()
            .NotEmpty()
            .WithMessage("Last name is required.");

        RuleFor(x=>x.Email).NotNull()
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Please provide a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

       RuleFor(x => x.DepartmentId).NotNull()
            .NotEmpty()
            .WithMessage("Department Id is required.");

       RuleFor(x => x.DesignationId).NotNull()
            .NotEmpty()
            .WithMessage("Designation Id is required.");
    }
}
