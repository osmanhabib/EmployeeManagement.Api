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

            if(authresult != null)
            {
                var response = new AuthenticationResponse(status: true,
                    message: "success",
                    Token: authresult.Token);
                return Ok(response);
            }

            var res = new ApiResponse() { status = false,
                   message = "Invalid login attempt." };

            return Ok(res);

        }
    }
}
