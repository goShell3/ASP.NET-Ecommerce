namespace Ecommerce.Application.Services.Authenticaton;

public interface IAuthenticationService {

    AuthnticationResult Login(string email, string token);
    AuthnticationResult Register(string firstName, string lastName, string email, string password);
    
}
