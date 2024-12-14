using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
    string GetTokenInfo(string token);
}
