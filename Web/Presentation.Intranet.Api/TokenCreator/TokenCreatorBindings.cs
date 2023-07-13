using Application.UserAuthorizationTokens;

namespace Presentation.Intranet.Api.TokenCreator
{
    public static class TokenCreatorBindings
    {
        public static IServiceCollection AddTokenCreatorBindings(this IServiceCollection services)
        {
            services.AddScoped<ITokenCreator, TokenCreator>();

            return services;
        }
    }
}
