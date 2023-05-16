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
            services.AddScoped<ICommandHandler<AddUserCommand>, AddUserCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUserLoginCommand>, UpdateUserLoginCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUserPasswordCommand>, UpdateUserPasswordCommandHandler>();

            services.AddScoped<IQueryHandler<IReadOnlyList<Event>, GetEventsQuery>, GetEventsQueryHandler>();
            services.AddScoped<IQueryHandler<User, GetUserByIdQuery>, GetUserByIdQueryHandler>();
            services.AddScoped<IQueryHandler<IReadOnlyList<User>, GetUsersQuery>, GetUsersQueryHandler>();
            return services;
        }
    }
}
