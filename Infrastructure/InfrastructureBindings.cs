using Application.Tokens;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.ConfigurationUtils.Token;
using Infrastructure.Entities.Events;
using Infrastructure.Entities.UserAuthorizationTokens;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureBindings
    {
        public static IServiceCollection AddInfrastructureBindings(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAuthorizationTokenRepository, UserAuthorizationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenConfiguration, TokenConfiguration>();
            return services;
        }
    }
}
