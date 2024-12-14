using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Features.Department.Queries;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Features.Department.Handlers;

public class GetDepartmentsQueryHandler(EmployeeManagementDbContext context)
{
    public async Task<GenericResponse<List<DepartmentResponse>>> HandleAsync(GetDepartmentsQuery query)
    {
        var departmentList = await context.Departments.ToListAsync();
        List<DepartmentResponse> departmentResponse = new List<DepartmentResponse>();

        foreach (var department in departmentList)
        {
            DepartmentResponse dept = new DepartmentResponse()
            {
                DepartmentId = department.Id,
                DepartmentName = department.DepartmentName,
            };

            departmentResponse.Add(dept);
        }

        return new GenericResponse<List<DepartmentResponse>>
        {
            message = "success",
            status = true,
            data = departmentResponse
        };
    }
}
