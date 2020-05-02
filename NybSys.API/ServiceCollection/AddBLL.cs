using Microsoft.Extensions.DependencyInjection;
using NybSys.AuditLog.BLL;
using NybSys.Auth.BLL;
using NybSys.Session.BLL;

namespace NybSys.API.ServiceCollection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddAuthenticationBLL();
            services.AddSessionBLL();
            services.AddAuditLogBLL();

            return services;
        }
    }
}
