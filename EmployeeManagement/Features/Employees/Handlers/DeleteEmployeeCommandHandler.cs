using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Extensions;
using EmployeeManagement.Features.Employees.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagement.Features.Employees.Handlers;

public class DeleteEmployeeCommandHandler(IValidator<DeleteEmployeeCommand> validator,
    EmployeeManagementDbContext context,
    IHttpContextAccessor httpContextAccessor)
{
    public async Task<ApiResponse> HandleAsync(DeleteEmployeeCommand command)
    {
        var claims = httpContextAccessor.HttpContext?.User.Claims.ToList() ?? throw new DataException("Logged in user is not valid.");

        var loggedInUserId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
        var loggedInUserRole = claims.FirstOrDefault(c => c.Type == "Role")?.Value ?? string.Empty;

        if (!loggedInUserRole.ToLower().Equals("admin")
            && !loggedInUserId.Equals(command.EmployeeId))
        {
            throw new DataException("User is not allowed to delete employee");
        }

        var validationResult = await validator.ValidateAsync(command);
        validationResult.EnsureValidResult();

        var existingEmployee = await context.Employees.FirstOrDefaultAsync(u => u.Id == command.EmployeeId);

        if (existingEmployee is null)
        {
            throw new DataException("No employee found");
        }

        context.Employees.Remove(existingEmployee);
        await context.SaveChangesAsync();

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id.Equals(loggedInUserId))
            ?? throw new DataException("User not found");

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return new ApiResponse() { message = "Employee deleted sucessfully", status = true };
    }
}
