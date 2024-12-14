using EmployeeManagement.Dtos;
using EmployeeManagement.Features.Employees.Commands;
using EmployeeManagement.Features.Employees.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("employee")]
    [ApiVersion("1.0")]
    public class EmployeeController(CreateEmployeeCommandHandler createEmployeeHandler) : ControllerBase
    {
        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateEmployee(CreateEmployeeCommand employeeCommand) 
        {
          var createdBy = User.Claims.ToList()[2].Value;

          employeeCommand.CreatedBy = createdBy ?? throw new DataException("Invalid Creator.");

          var result = createEmployeeHandler.HandleAsync(employeeCommand).Result;

          return Ok(result);
        }
    }
}
