using Application.Interfaces;
using Application.UserAuthorizationTokens.Commands.UserAuthorization;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UserAuthorizationTokens
{
    public static class UserAuthorizationTokenBindings
    {
        public static IServiceCollection AddUserAuthorizationTokenBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<UserAuthorizationCommand>, AuthorizationUserQueryHandler>();
            services.AddScoped<IAsyncValidator<UserAuthorizationCommand>, AuthorizationUserQueryValidator>();

            return services;
        }
    }
}
