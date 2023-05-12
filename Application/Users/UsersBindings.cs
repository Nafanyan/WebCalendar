using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUserLogin;
using Application.Users.Commands.UpdateUserPassword;
using Application.Users.Queries.GetEvents;
using Application.Users.Queries.GetUserById;
using Microsoft.Extensions.DependencyInjection;


namespace WebCalendar.Application.Users
{
    public static class UsersBindings
    {
        public static IServiceCollection AddUsersBindings(this IServiceCollection services)
        {
            services.AddScoped<IAddUserCommandHandler, AddUserCommandHandler>();
            services.AddScoped<IDeleteUserCommandHandler, DeleteUserCommandHandler>();
            services.AddScoped<IUpdateUserLoginCommandHandler, UpdateUserLoginCommandHandler>();
            services.AddScoped<IUpdateUserPasswordCommandHandler, UpdateUserPasswordCommandHandler>();

            services.AddScoped<IGetEventsQueryHandler, GetEventsQueryHandler>();
            services.AddScoped<IGetUserByIdQueryHandler, GetUserByIdQueryHandler>();
            services.AddScoped<IGetUserByIdQueryHandler, GetUserByIdQueryHandler>();
            return services;
        }
    }
}
