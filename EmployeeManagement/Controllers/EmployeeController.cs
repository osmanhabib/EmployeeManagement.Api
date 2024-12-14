using EmployeeManagement.Features.Employees.Commands;
using EmployeeManagement.Features.Employees.Handlers;
using EmployeeManagement.Features.Employees.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("employee")]
    [ApiVersion("1.0")]
    [Authorize]
    public class EmployeeController(CreateEmployeeCommandHandler createEmployeeHandler,
        GetEmployeeQueryHandler getEmployeeQueryHandler,
        UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
        DeleteEmployeeCommandHandler deleteEmployeeCommandHandler) : ControllerBase
    {
        [HttpPost("create")]
        [AllowAnonymous]
        public IActionResult CreateEmployee([FromBody]CreateEmployeeCommand command) 
        {
          var result = createEmployeeHandler.HandleAsync(command).Result;

          return Ok(result);
        }

        [HttpGet("get")]
        public IActionResult GetEmployee([FromQuery]GetEmployeeQuery query)
        {
            var result = getEmployeeQueryHandler.HandleAsync(query).Result;

            return Ok(result);
        }

        [HttpPut("update")]
        public IActionResult UpdateEmployee([FromBody]UpdateEmployeeCommand command)
        {
            var result = updateEmployeeCommandHandler.HandleAsync(command).Result;

            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteEmployee([FromBody]DeleteEmployeeCommand command)
        {
            var result = deleteEmployeeCommandHandler.HandleAsync(command).Result;

            return Ok(result);
        }
    }
}
