using BusinessLayer.Auth;
using BusinessLayerAbstraction.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer
{
    public static class BusinessExtension
    {
        public static IServiceCollection BusinessLibararyExtension(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            return services;
        }
    }
}
