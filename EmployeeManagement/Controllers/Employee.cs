using EmployeeManagement.Features.Employees.Commands;
using EmployeeManagement.Features.Employees.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("employee")]
    public class Employee(CreateEmployeeCommandHandler createEmployeeHandler) : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateEmployee(CreateEmployeeCommand employeeCommand) 
        {
          employeeCommand.CreatedBy = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value ?? throw new Exception("Invalid Creator.");
          var result = createEmployeeHandler.HandleAsync(employeeCommand).Result;
          return Ok(result);
        }
    }
}
