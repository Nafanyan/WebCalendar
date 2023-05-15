using Application.Users.Commands;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUserLogin;
using Application.Users.Commands.UpdateUserPassword;
using Application.Users.Queries;
using Application.Users.Queries.GetEvents;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUsers;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace WebCalendar.Application.Users
{
    public static class UsersBindings
    {
        public static IServiceCollection AddUsersBindings(this IServiceCollection services)
        {
            services.AddScoped<IUserCommandHandler<AddUserCommand>, AddUserCommandHandler>();
            services.AddScoped<IUserCommandHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
            services.AddScoped<IUserCommandHandler<UpdateUserLoginCommand>, UpdateUserLoginCommandHandler>();
            services.AddScoped<IUserCommandHandler<UpdateUserPasswordCommand>, UpdateUserPasswordCommandHandler>();

            services.AddScoped<IUserQueryHandler<IReadOnlyList<Event>, GetEventsQuery>, GetEventsQueryHandler>();
            services.AddScoped<IUserQueryHandler<User, GetUserByIdQuery>, GetUserByIdQueryHandler>();
            services.AddScoped<IUserQueryHandler<IReadOnlyList<User>, GetUsersQuery>, GetUsersQueryHandler>();
            return services;
        }
    }
}
