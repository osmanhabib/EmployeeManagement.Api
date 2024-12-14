using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Features.Employees.Queries;
using FluentValidation;
using System.Data;
using EmployeeManagement.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Features.Employees.Handlers
{
    public class GetEmployeeQueryHandler(IValidator<GetEmployeeQuery> validator,
    EmployeeManagementDbContext context,
    IHttpContextAccessor httpContextAccessor)
    {
        public async Task<GenericResponse<EmployeeResponse>> HandleAsync(GetEmployeeQuery query)
        {
            var claims = httpContextAccessor.HttpContext?.User.Claims.ToList() ?? throw new DataException("Logged in user is not valid.");

            var loggedInUserId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
            var loggedInUserRole = claims.FirstOrDefault(c => c.Type == "Role")?.Value ?? string.Empty;

            if (!loggedInUserRole.ToLower().Equals("admin")
                && !loggedInUserId.Equals(query.EmployeeId))
            {
                throw new DataException("User is not allowed to get employee details");
            }

            var validationResult = await validator.ValidateAsync(query);
            validationResult.EnsureValidResult();

            var existingEmployee = await context.Employees.FirstOrDefaultAsync(u => u.Id == query.EmployeeId);

            if (existingEmployee is null)
            {
                throw new DataException("No employee found");
            }

            var department = await context.Departments.FirstOrDefaultAsync(d => d.Id.Equals(existingEmployee.DepartmentId))
                    ?? throw new DataException("No department found");

            var designation = await context.Designations.FirstOrDefaultAsync(d=> d.Id.Equals(existingEmployee.DesignationId))
                    ?? throw new DataException("No designation found");

            var response = new GenericResponse<EmployeeResponse>()
            {
                message = "success",
                status = true,
                data = new EmployeeResponse()
                {
                    EmployeeId = existingEmployee.Id,
                    FirstName = existingEmployee.FirstName,
                    LastName = existingEmployee.LastName,
                    DepartmentId = department.Id ?? string.Empty,
                    DepartmentName = department.DepartmentName,
                    DesignationId = designation.Id,
                    DesignationName = designation.DesignationName,
                }
            };

            return response;
        }
    }
}
