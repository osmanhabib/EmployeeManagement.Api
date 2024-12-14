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
    EmployeeManagementDbContext context,
    IHttpContextAccessor httpContextAccessor)
{
    public async Task<ApiResponse> HandleAsync(CreateEmployeeCommand command)
    {
        var claims = httpContextAccessor.HttpContext?.User.Claims.ToList() ?? throw new DataException("Logged in user is not valid.");
       
        var loggedInUserId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        var loggedInUserRole = claims.FirstOrDefault(c => c.Type == "Role")?.Value;

        if(loggedInUserId  == null || loggedInUserRole == null)
        {
            throw new DataException("User is not valid to create a new employee");
        }

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
            return new ApiResponse() { message = "Invalid Department Id", status = false };
        }

        var isValidDesignation = await context.Designations
            .SingleOrDefaultAsync(d => d.Id.Equals(command.DesignationId));

        if (isValidDesignation == null)
        {
            return new ApiResponse() { message = "Invalid Designation Id", status = false };
        }

        var user = new User()
        {
            UserName = command.Email,
            Password = Utility.StringToBase64(command.Password),
            CreatedBy = command.CreatedBy,
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
            FirstName = command.Email,
            LastName = command.LastName,
            CreatedBy = command.CreatedBy,
            DepartmentId = command.DepartmentId,
            DesignationId = command.DesignationId,
            Email = command.Email
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();

        return new ApiResponse() { message = "Successfully created.", status = true };
    }

}
