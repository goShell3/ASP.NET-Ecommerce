namespace Ecommerce.Constructs.Authentication;

public record LoginRequest (
    string Email,
    string Password
);