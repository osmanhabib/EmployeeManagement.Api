using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using System.Data;

namespace EmployeeManagement.Services.Implementations
{
    public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,
        EmployeeManagementDbContext  context) : IAuthenticationService
    {
        public AuthenticationResult Login(string email, string password)
        {
            var user =  context.Users.SingleOrDefault(u=> u.UserName.Equals(email) && u.Password.Equals(password)) 
                   ?? throw new InvalidDataException("Invalid User");

            if (user == null) 
            {
                throw new DataException("Invalid User");
            }
            var token = jwtTokenGenerator.GenerateToken(user);
            
            return new AuthenticationResult(token);
        }
    }
}
