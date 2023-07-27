using Application.UserAuthorizationTokens;
using Microsoft.Extensions.DependencyInjection;
using WebCalendar.Application.Events;
using WebCalendar.Application.Users;

namespace Application
{
    public static class ApplicationBindings
    {
        public static IServiceCollection AddApplicationBindings( this IServiceCollection services )
        {
            services.AddEventsBindings();
            services.AddUsersBindings();
            services.AddUserAuthorizationTokenBindings();

            return services;
        }
    }
}
