using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Utilities;
using System.Data;

namespace EmployeeManagement.Services.Implementations
{
    public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,
        EmployeeManagementDbContext  context) : IAuthenticationService
    {
        public AuthenticationResult Login(string email, string password)
        {
            var user =  context.Users.SingleOrDefault(u=> u.UserName.Equals(email)) 
                   ?? throw new InvalidDataException("Invalid User");

            var validUserLogin = user !=null && Utility.Base64ToString(user.Password).Equals(password);

            if (user == null || !validUserLogin) 
            {
                throw new DataException("Invalid login attempt");
            }
            var token = jwtTokenGenerator.GenerateToken(user);
            
            return new AuthenticationResult(token);
        }
    }
}
