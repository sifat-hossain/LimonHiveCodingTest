using System;
using Identity.Http.Extensions.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Http.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityContext(this IServiceCollection services,
           Action<IdentityContextOptions> setupAction)
        {
            if (setupAction == null) throw new ArgumentNullException();

            services.Configure(setupAction);

            services.AddHttpContextAccessor();

            services.AddScoped<IAuthorizationTokenProvider, HttpRequestAuthorizationTokenProvider>();

            services.AddScoped<IIdentityContext, JwtIdentityContext>();

            return services;
        }
    }
}