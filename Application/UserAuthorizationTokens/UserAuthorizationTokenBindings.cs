using Application.Interfaces;
using Application.UserAuthorizationTokens.Commands.CreateToken;
using Application.UserAuthorizationTokens.Commands.VerificationToken;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UserAuthorizationTokens
{
    public static class UserAuthorizationTokenBindings
    {
        public static IServiceCollection AddUserAuthorizationTokenBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateTokenCommand>, CreateTokenCommandHandler>();
            services.AddScoped<ICommandHandler<VerificationTokenCommand>, VerificationTokenCommandHandler>();

            services.AddScoped<IAsyncValidator<CreateTokenCommand>, CreateTokenCommandValidator>();
            services.AddScoped<IAsyncValidator<VerificationTokenCommand>, VerificationTokenCommandValidator>();

            return services;
        }
    }
}
