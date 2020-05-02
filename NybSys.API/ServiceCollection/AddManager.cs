using Microsoft.Extensions.DependencyInjection;
using NybSys.API.Manager;

namespace NybSys.API.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddManager(this IServiceCollection services)
        {
            services.AddScoped<ISecurityManager, SecurityManager>();
            services.AddScoped<IUserManager, SecurityManager>();
            services.AddScoped<IAccessManager, SecurityManager>();
            services.AddScoped<ILogManager, LogManager>();

            return services;
        }
    }
}
