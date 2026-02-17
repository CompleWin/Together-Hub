using System.Security.Claims;
using System.Text;
using Application.Security.Services;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security.Services;

public class JwtSecurityService(IConfiguration configuration) : IJwtSecurityService
{
    public string CreateToken(CustomIdentityUser user)
    {
        string secretKey = configuration["AuthSettings:SecretKey"]!;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new("is_premium", (user.FullName.ToString().Length % 2 == 0).ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenHandler = new JsonWebTokenHandler();
        var tokenDiscriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = creds,
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(0),
            Expires = DateTime.UtcNow.AddMinutes(1),
        };

        var token = tokenHandler.CreateToken(tokenDiscriptor);
        return token;
    }
}