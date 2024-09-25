using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using university.DAL.Enums;

namespace university.Services;

public class JwtCreator
{
    private readonly string securityKeyString;

    public JwtCreator(string securityKeyString)
    {
        this.securityKeyString = securityKeyString;
    }

    public string CreateToken(int userId, Role role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyString));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("UserId", userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var token = new JwtSecurityToken(
            "WebApp",
            "WebApp",
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}