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
            var loggedInUserEmail = claims.FirstOrDefault(c => c.Type == "Email")?.Value ?? string.Empty;

            var existingEmployee = await context.Employees.FirstOrDefaultAsync(u => u.Email.Equals(loggedInUserEmail));

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
                    Email = existingEmployee.Email,
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
