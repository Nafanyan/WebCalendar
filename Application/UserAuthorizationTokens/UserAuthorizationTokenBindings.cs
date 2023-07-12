using Application.Interfaces;
using Application.UserAuthorizationTokens.Commands;
using Application.UserAuthorizationTokens.Commands.AuthenticateUser;
using Application.UserAuthorizationTokens.Commands.RefreshToken;
using Application.UserAuthorizationTokens.DTOs;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UserAuthorizationTokens
{
    public static class UserAuthorizationTokenBindings
    {
        public static IServiceCollection AddUserAuthorizationTokenBindings(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationCommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand>, AuthenticateUserCommandHandler>();
            services.AddScoped<IAuthorizationCommandHandler<RefreshTokenCommandDto, RefreshTokenCommand>, RefreshTokenCommandHandler>();

            services.AddScoped<IAsyncValidator<AuthenticateUserCommand>, AuthenticateUserCommandValidator>();
            services.AddScoped<IAsyncValidator<RefreshTokenCommand>, RefreshTokenCommandValidator>();

            return services;
        }
    }
}
