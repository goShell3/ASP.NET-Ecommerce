using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;

namespace Ecommerce.Application.Services.Authenticaton;

public class AuthenticationController : IAuthenticationService
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationController(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

     public AuthnticationResult Register(string firstName, string lastName, string email, string password)
   {
    // check if user exists
    if(_userRepository.GetUserByEmail(email) is not null) {
        throw new Exception("User already registered");
    }
    // check if token is valid

    var user = new User (
        Guid.NewGuid(),
        firstName,
        lastName,
        email,
        password
        
    );

    _userRepository.AddUser(user);

    // generate JWT token

    var token = _jwtTokenGenerator.GenerateToken(user.Id, firstName, lastName);

    return new AuthnticationResult(
        user.Id,
        firstName,
        lastName,
        email, 
        token
     );
   }

    public AuthnticationResult Login(string email, string token, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User not found");
        };
       
        if (user.Password != password)
        {
            throw new Exception("Invalid token");
        };

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);


        return new AuthnticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            email,
            token);
    }
}
