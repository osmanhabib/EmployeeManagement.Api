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
        var loggedInUserRole = claims.FirstOrDefault(c => c.Type == "Role")?.Value ?? string.Empty;

        if(!loggedInUserRole.ToLower().Equals("admin") 
            && !loggedInUserId.Equals(command.EmployeeId))
        {
            throw new DataException("User is not allowed to update employee");
        }

        var validationResult = await validator.ValidateAsync(command);
        validationResult.EnsureValidResult();

        if(!string.IsNullOrEmpty(command.DepartmentId))
        {
            var isValidDepartment = await context.Departments
           .SingleOrDefaultAsync(d => d.Id.Equals(command.DepartmentId));

            if (isValidDepartment == null)
            {
                return new ApiResponse() { message = "Invalid Department Id", status = false };
            }
        }

        if(!string.IsNullOrEmpty(command.DesignationId))
        {
            var isValidDesignation = await context.Designations
            .SingleOrDefaultAsync(d => d.Id.Equals(command.DesignationId));

            if (isValidDesignation == null)
            {
                return new ApiResponse() { message = "Invalid Designation Id", status = false };
            }
        }

        var existingEmployee = await context.Employees.FirstOrDefaultAsync(u => u.Id == command.EmployeeId);

        if (existingEmployee is null)
        {
            throw new DataException("No employee found");
        }

        existingEmployee.ModifiedOn = DateTime.Now;
        existingEmployee.ModifiedBy = loggedInUserId;
        existingEmployee.FirstName = command.FirstName ?? existingEmployee.FirstName;
        existingEmployee.LastName = command.LastName ?? existingEmployee.LastName;
        existingEmployee.DepartmentId = command.DepartmentId ?? existingEmployee.DepartmentId;
        existingEmployee.DesignationId = command.DesignationId ?? existingEmployee.DesignationId;

        context.Employees.Update(existingEmployee);
        await context.SaveChangesAsync();

        return new ApiResponse() { message = "Employee updated sucessfully", status = true };
    }
}
