using EmployeeManagement.Dtos;

namespace EmployeeManagement.Services;

public interface IAuthenticationService
{
    AuthenticationResult Login(string email,
    string password);
}
