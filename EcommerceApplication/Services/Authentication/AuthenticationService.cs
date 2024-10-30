using Ecommerce.Application.Common.Interfaces.Authentication;

namespace Ecommerce.Application.Services.Authenticaton;

public class AuthenticationController : IAuthenticationService
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationController(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

     public AuthnticationResult Register(string firstName, string lastName, string email, string token)
   {
    // check if user exists

    // check if token is valid
    // generate JWT token
    // return authentication result
    Guid userId = Guid.NewGuid();

    token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

    return new AuthnticationResult(
        userId,
        firstName,
        lastName,
        email, 
        token
     );
   }

    public AuthnticationResult Login(string email, string token)
    {

        
        return new AuthnticationResult(
            Guid.NewGuid(),
            "FirstName",
            "LastName",
            email, 
            "token"
        );
    }

  
}