using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Features.Designation.Queries;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Features.Designation.Handlers;

public class GetDesignationsQueryHandler(EmployeeManagementDbContext context)
{
    public async Task<GenericResponse<List<DesignationResponse>>> HandleAsync(GetDesignationsQuery query)
    {
        var designationList = await context.Designations.ToListAsync();
        List <DesignationResponse> designationResponse = new List <DesignationResponse>();

        foreach (var designation in designationList)
        {
            DesignationResponse desig = new DesignationResponse()
            {
                DesignationId = designation.Id,
                DesignationName = designation.DesignationName,
            };

            designationResponse.Add(desig);
        }

        return new GenericResponse<List<DesignationResponse>>
        {
            message = "success",
            status = true,
            data = designationResponse
        };
    }
}
