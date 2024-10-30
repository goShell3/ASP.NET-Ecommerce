using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using 


namespace Ecommerce.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator {
    public string GenerateToken(Guid userId, string firstName, string lastName) {
        // Implement JWT token generation logic here

    var signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("Super-private-key")),
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