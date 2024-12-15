using Asp.Versioning;
using EmployeeManagement.Features.Designation.Handlers;
using EmployeeManagement.Features.Designation.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/designation")]
    [ApiVersion("1.0")]
    public class DesignationController(GetDesignationsQueryHandler getDesignationsQueryHandler) : ControllerBase
    {
        [HttpGet("getlist")]
        public IActionResult GetDesignations([FromQuery]GetDesignationsQuery query) 
        {
          var result = getDesignationsQueryHandler.HandleAsync(query).Result;

          return Ok(result);
        }
    }
}
