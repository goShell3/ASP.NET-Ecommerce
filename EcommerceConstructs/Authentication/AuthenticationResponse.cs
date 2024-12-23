namespace Ecommerce.Constructs.Authentication;

public record AuthenticationResponse (
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string token
);