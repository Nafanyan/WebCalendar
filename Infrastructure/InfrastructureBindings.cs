using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Data.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureBindings
    {
        public static IServiceCollection AddInfrastructureBindings(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, IUserRepository>();
            services.AddScoped<IUnitOfWork, IUnitOfWork>();
            return services;
        }
    }
}
