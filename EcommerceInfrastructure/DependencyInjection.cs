using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Services.Authenticaton;
using Ecommerce.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        // services.AddScoped<IAuthenticationService, AuthenticationService>();

        // services.AddScoped<IProductRepository, ProductRepository>();
        // services.AddScoped<IOrderRepository, OrderRepository>();
        // services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthenticationService,AuthenticationService>();

        return services;
    }
}
