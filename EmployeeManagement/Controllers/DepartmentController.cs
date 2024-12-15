using Asp.Versioning;
using EmployeeManagement.Features.Department.Handlers;
using EmployeeManagement.Features.Department.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/department")]
    [ApiVersion("1.0")]
    public class DepartmentController(GetDepartmentsQueryHandler getDepartmentsQueryHandler) : ControllerBase
    {
        [HttpGet("getlist")]
        public IActionResult GetDepartments([FromQuery]GetDepartmentsQuery query) 
        {
          var result = getDepartmentsQueryHandler.HandleAsync(query).Result;

          return Ok(result);
        }
    }
}
