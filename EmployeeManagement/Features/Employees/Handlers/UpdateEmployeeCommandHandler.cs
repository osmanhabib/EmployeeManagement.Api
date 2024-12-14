using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Extensions;
using EmployeeManagement.Features.Employees.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagement.Features.Employees.Handlers;

public class UpdateEmployeeCommandHandler(IValidator<UpdateEmployeeCommand> validator,
    EmployeeManagementDbContext context,
    IHttpContextAccessor httpContextAccessor)
{
    public async Task<ApiResponse> HandleAsync(UpdateEmployeeCommand command)
    {
        var claims = httpContextAccessor.HttpContext?.User.Claims.ToList() ?? throw new DataException("Logged in user is not valid.");

        var loggedInUserId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
        var loggedUserEmail = claims.FirstOrDefault(c => c.Type == "Email")?.Value ?? string.Empty;

        var validationResult = await validator.ValidateAsync(command);
        validationResult.EnsureValidResult();

        var existingEmployee = await context.Employees.FirstOrDefaultAsync(u => u.Email.Equals(loggedUserEmail));

        if (existingEmployee is null)
        {
            throw new DataException("No employee found");
        }

        if (!string.IsNullOrEmpty(command?.FirstName))
        {
            existingEmployee.FirstName = command.FirstName;
        }

        if(!string.IsNullOrEmpty(command?.LastName))
        { 
            existingEmployee.LastName = command.LastName;
        }

        if(!string.IsNullOrEmpty(command?.DepartmentId))
        {
            var isValidDepartment = await context.Departments
           .SingleOrDefaultAsync(d => d.Id.Equals(command.DepartmentId));

            if (isValidDepartment == null)
            {
                return new ApiResponse() { message = "Invalid Department Id", status = false };
            }
            else
            {
                existingEmployee.DepartmentId = command.DepartmentId;
            }
        }

        if(!string.IsNullOrEmpty(command?.DesignationId))
        {
            var isValidDesignation = await context.Designations
            .SingleOrDefaultAsync(d => d.Id.Equals(command.DesignationId));

            if (isValidDesignation == null)
            {
                return new ApiResponse() { message = "Invalid Designation Id", status = false };
            }
            else
            {
                existingEmployee.DesignationId = command.DesignationId;
            }
        }

        existingEmployee.ModifiedOn = DateTime.Now;
        existingEmployee.ModifiedBy = loggedInUserId;


        context.Employees.Update(existingEmployee);
        await context.SaveChangesAsync();

        return new ApiResponse() { message = "Employee updated sucessfully", status = true };
    }
}
