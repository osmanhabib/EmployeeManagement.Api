using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Services.Implementations;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtSettings) : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        var signingCredientials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("UserId",user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
            new Claim("Role", user.Role),
        };

        var securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Value.Issuer,
            audience: jwtSettings.Value.Audience,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.Value.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredientials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GetTokenInfo(string token)
    {
        throw new NotImplementedException();
    }
}
