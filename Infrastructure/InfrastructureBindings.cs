
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Data.Events;
using Infrastructure.Data.Users;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
