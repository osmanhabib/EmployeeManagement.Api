using EmployeeManagement.Features.Department.Handlers;
using EmployeeManagement.Features.Department.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("department")]
    [ApiVersion("1.0")]
    public class DepartmentController(GetDepartmentsQueryHandler getDepartmentsQueryHandler) : ControllerBase
    {
        [HttpGet("getlist")]
        [Authorize]
        public IActionResult GetDepartments([FromQuery]GetDepartmentsQuery query) 
        {
          var result = getDepartmentsQueryHandler.HandleAsync(query).Result;

          return Ok(result);
        }
    }
}
