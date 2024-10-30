namespace Ecommerce.Application.Services.Authenticaton;

public record AuthnticationResult (
    Guid Id,
    string firstName,
    string lastName,
    string email,
    string token
);