using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator {
    public string GenerateToken(Guid userId, string firstName, string lastName) {

    var signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("123456783456765439876543123456789")),
        SecurityAlgorithms.HmacSha256
    );

    var claims = new [] {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName), 
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
           
        };
    var securityToken = new JwtSecurityToken(
        issuer: "Ecommerce",
        // audience: "https://localhost:5002",
        signingCredentials: signingCredentials,
        expires: DateTime.UtcNow.AddHours(1),
        notBefore: DateTime.UtcNow,
        claims: claims);

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    
}
