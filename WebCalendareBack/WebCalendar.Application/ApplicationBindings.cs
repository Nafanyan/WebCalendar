using Microsoft.Extensions.DependencyInjection;
using WebCalendar.Application.Events;
using WebCalendar.Application.Users;

namespace WebCalendar.Application
{
    public static class ApplicationBindings
    {
        public static IServiceCollection AddApplicationBindings(this IServiceCollection services)
        {
            services.AddEventsBindings();
            services.AddUsersBindings();

            return services;
        }
    }
}
