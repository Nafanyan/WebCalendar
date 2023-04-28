using Microsoft.Extensions.DependencyInjection;
using WebCalendar.Application.Users.UserDeleting;
using WebCalendar.Application.Users.UsersCreating;
using WebCalendar.Application.Users.UsersRecieving;
using WebCalendar.Application.Users.UserUpdating;

namespace WebCalendar.Application.Users
{
    public static class UsersBindings
    {
        public static IServiceCollection AddUsersBindings(this IServiceCollection services)
        {
            services.AddScoped<IUserCreator, UserCreator>();
            services.AddScoped<IUserDeleter, UserDeleter>();
            services.AddScoped<IUserReciever, UserReciever>();
            services.AddScoped<IUserUpdater, UserUpdater>();

            return services;
        }
    }
}
