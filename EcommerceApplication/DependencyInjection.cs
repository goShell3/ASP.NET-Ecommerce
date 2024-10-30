using Ecommerce.Application.Services.Authenticaton;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, IAuthenticationService>();

        return services;
    }
}
