using EmployeeManagement.Dtos;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{

    [ApiController]
    [Route("auth")]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var authresult = authenticationService.Login(loginRequest.Email,
                loginRequest.Password);

            var response = new AuthenticationResponse(authresult.Token);

            return Ok(response);
        }
    }
}
