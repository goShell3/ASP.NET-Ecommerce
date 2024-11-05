using Ecommerce.Application.Contracts;
using Ecommerce.Application.Services.Authenticaton;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, IAuthenticationService>();
            services.AddSingleton<IProductService>();
            return services;
        }
    }
}
