using EmployeeManagement.Features.Designation.Handlers;
using EmployeeManagement.Features.Designation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("designation")]
    [ApiVersion("1.0")]
    public class DesignationController(GetDesignationsQueryHandler getDesignationsQueryHandler) : ControllerBase
    {
        [HttpGet("getlist")]
        [Authorize]
        public IActionResult GetDesignations([FromQuery]GetDesignationsQuery query) 
        {
          var result = getDesignationsQueryHandler.HandleAsync(query).Result;

          return Ok(result);
        }
    }
}
