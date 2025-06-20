﻿using Application.Interfaces;
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
            services.AddScoped<ICommandHandler<RefreshTokenCommandDto, RefreshTokenCommand>, RefreshTokenCommandHandler>();
            services.AddScoped<IAsyncValidator<RefreshTokenCommand>, RefreshTokenCommandValidator>();

            return services;
        }
    }
}
