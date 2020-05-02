using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NybSys.Auth.BLL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuthenticationBLL(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationBLL, AuthenticationBLL>();
            services.AddScoped<ISessionLogBLL, SessionLogBLL>();
            services.AddScoped<IAccessRightBLL, AccessRightBLL>();
            services.AddScoped<IUserBLL, UserBLL>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}