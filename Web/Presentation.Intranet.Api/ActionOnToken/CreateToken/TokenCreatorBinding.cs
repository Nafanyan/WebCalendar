using Application.UserAuthorizationTokens;

namespace Presentation.Intranet.Api.ActionOnToken.CreateToken
{
    public static class ActionOnTokenBinding
    {
        public static IServiceCollection AddTokenCreatorBinding(this IServiceCollection services)
        {
            services.AddScoped<ITokenCreator, TokenCreator>();
            return services;
        }
    }
}
