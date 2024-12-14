using EmployeeManagement.Data;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Dtos;
using EmployeeManagement.Extensions;
using EmployeeManagement.Features.Employees.Commands;
using EmployeeManagement.Utilities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagement.Features.Employees.Handlers;

public class CreateEmployeeCommandHandler(IValidator<CreateEmployeeCommand> validator,
    EmployeeManagementDbContext context)
{
    public async Task<ApiResponse> HandleAsync(CreateEmployeeCommand command)
    {
        var validationResult = await validator.ValidateAsync(command);
        validationResult.EnsureValidResult();

        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == command.Email);

        if (existingUser is not null)
        {
            throw new DataException("User is already exists");
        }

        var isValidDepartment = await context.Departments
            .SingleOrDefaultAsync(d => d.Id.Equals(command.DepartmentId));

        if (isValidDepartment == null)
        {
            throw new DataException("Invalid Department Id");
        }

        var isValidDesignation = await context.Designations
            .SingleOrDefaultAsync(d => d.Id.Equals(command.DesignationId));

        if (isValidDesignation == null)
        {
            throw new DataException("Invalid Designation Id");
        }

        var user = new User()
        {
            UserName = command.Email,
            Password = Utility.StringToBase64(command.Password),
            CreatedBy = "system",
            Role = "user"
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        if (user is null || user.UserId <1)
        {
            return new ApiResponse() { message = "Failed to create employee", status = false };
        }

        var employee = new Employee()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            CreatedBy = "system",
            DepartmentId = command.DepartmentId,
            DesignationId = command.DesignationId,
            Email = command.Email
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();

        return new ApiResponse() { message = "Employee successfully created.", status = true };
    }

}
