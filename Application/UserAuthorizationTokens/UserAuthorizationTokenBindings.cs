using Application.Interfaces;
using Application.UserAuthorizationTokens.DTOs;
using Application.Users.DTOs;
using Application.Users.Queries.GetUserById;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UserAuthorizationTokens
{
    public static class UserAuthorizationTokenBindings
    {
        public static IServiceCollection AddUserAuthorizationTokenBindings(this IServiceCollection services)
        {
            services.AddScoped<IQueryHandler<GetTokenQueryDto, UserAuthorizationQuery>, AuthorizationUserQueryHandler>();
            services.AddScoped<IAsyncValidator<UserAuthorizationQuery>, AuthorizationUserQueryValidator>();

            return services;
        }
    }
}
