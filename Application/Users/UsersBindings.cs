using Application.Interfaces;
using Application.Users.Commands;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUserLogin;
using Application.Users.Commands.UpdateUserPassword;
using Application.Users.Queries;
using Application.Users.Queries.GetEvents;
using Application.Users.Queries.GetUserById;
using Application.Validation;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace WebCalendar.Application.Users
{
    public static class UsersBindings
    {
        public static IServiceCollection AddUsersBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUserLoginCommand>, UpdateUserLoginCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUserPasswordCommand>, UpdateUserPasswordCommandHandler>();

            services.AddScoped<IQueryHandler<IReadOnlyList<Event>, GetEventsQuery>, GetEventsQueryHandler>();
            services.AddScoped<IQueryHandler<User, GetUserByIdQuery>, GetUserByIdQueryHandler>();

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidation>();
            services.AddScoped<IValidator<DeleteUserCommand>, DeleteUserCommandValidation>();
            services.AddScoped<IValidator<UpdateUserLoginCommand>, UpdateUserLoginCommandValidation>();
            services.AddScoped<IValidator<UpdateUserPasswordCommand>, UpdateUserPasswordCommandValidation>();

            services.AddScoped<IValidator<GetEventsQuery>, GetEventsQueryValidation>();
            services.AddScoped<IValidator<GetUserByIdQuery>, GetUserByIdQueryValidation>();
            return services;
        }
    }
}
