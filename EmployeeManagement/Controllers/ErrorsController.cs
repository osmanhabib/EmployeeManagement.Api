using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace EmployeeManagement.Controllers;

[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var path = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Path ?? string.Empty;
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        Log.Error(path, exception?.Message, DateTime.Now);

        return Problem(title: exception?.Message,
            statusCode: (int)HttpStatusCode.BadRequest);
    }
}
