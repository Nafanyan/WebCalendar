using Application.Interfaces;
using Application.UserAuthorizationTokens.Commands.CreateToken;
using Application.UserAuthorizationTokens.Commands.TokenVerification;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UserAuthorizationTokens
{
    public static class UserAuthorizationTokenBindings
    {
        public static IServiceCollection AddUserAuthorizationTokenBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<TokenCreateCommand>, TokenCreateCommandHandler>();
            services.AddScoped<ICommandHandler<TokenVerificationCommand>, TokenVerificationCommandHandler>();

            services.AddScoped<IAsyncValidator<TokenCreateCommand>, TokenCreateCommandValidator>();
            services.AddScoped<IAsyncValidator<TokenVerificationCommand>, TokenVerificationCommandValidator>();

            return services;
        }
    }
}
