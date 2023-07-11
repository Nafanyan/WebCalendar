using Application.Interfaces;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUserLogin;
using Application.Users.Commands.UpdateUserPassword;
using Application.Users.DTOs;
using Application.Users.Queries.EventsQuery;
using Application.Users.Queries.QueryUserById;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace WebCalendar.Application.Users
{
    public static class UsersBindings
    {
        public static IServiceCollection AddUsersBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<UserCreateCommand>, UserCreateCommandHandler>();
            services.AddScoped<ICommandHandler<UserDeleteCommand>, UserDeleteCommandHandler>();
            services.AddScoped<ICommandHandler<UserLoginUpdateCommand>, UserLoginUpdateCommandHandler>();
            services.AddScoped<ICommandHandler<UserPasswordUpdateCommand>, UserPasswordUpdateCommandHandler>();

            services.AddScoped<IQueryHandler<IReadOnlyList<EventsQueryDto>, EventsQuery>, EventsQueryHandler>();
            services.AddScoped<IQueryHandler<UserQueryByIdDto, UserQueryById>, UserQueryHandlerById>();

            services.AddScoped<IAsyncValidator<UserCreateCommand>, UserCreateCommandValidator>();
            services.AddScoped<IAsyncValidator<UserDeleteCommand>, UserDeleteCommandValidator>();
            services.AddScoped<IAsyncValidator<UserLoginUpdateCommand>, UserLoginUpdateCommandValidator>();
            services.AddScoped<IAsyncValidator<UserPasswordUpdateCommand>, UserPasswordUpdateCommandValidator>();

            services.AddScoped<IAsyncValidator<EventsQuery>, EventsQueryValidator>();
            services.AddScoped<IAsyncValidator<UserQueryById>, UserQueryValidatorById>();
            return services;
        }
    }
}
