using EmployeeManagement.Data;
using EmployeeManagement.Dtos;

namespace EmployeeManagement.Services.Implementations
{
    public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,
        EmployeeManagementDbContext  context) : IAuthenticationService
    {
        public AuthenticationResult Login(string email, string password)
        {
            var user =  context.Users.SingleOrDefault(u=> u.UserName == email && u.Password == password);
            if (user == null) 
            {
                var token = jwtTokenGenerator.GenerateToken(user);
            }
            throw new Exception("Invalid User");
        }
    }
}
