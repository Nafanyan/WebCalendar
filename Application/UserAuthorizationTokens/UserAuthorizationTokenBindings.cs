using Microsoft.Extensions.DependencyInjection;

namespace Application.UserAuthorizationTokens
{
    public static class UserAuthorizationTokenBindings
    {
        public static IServiceCollection AddJWTBindings(this IServiceCollection services)
        {
            return services;
        }
    }
}
