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
            services.AddScoped<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUserLoginCommand>, UpdateUserLoginCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUserPasswordCommand>, UpdateUserPasswordCommandHandler>();

            services.AddScoped<IQueryHandler<IReadOnlyList<GetEventsQueryDto>, GetEventsQuery>, GetEventsQueryHandler>();
            services.AddScoped<IQueryHandler<GetUserByIdQueryDto, GetUserByIdQuery>, GetUserByIdQueryHandler>();

            services.AddScoped<IAsyncValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IAsyncValidator<DeleteUserCommand>, DeleteUserCommandValidator>();
            services.AddScoped<IAsyncValidator<UpdateUserLoginCommand>, UpdateUserLoginCommandValidator>();
            services.AddScoped<IAsyncValidator<UpdateUserPasswordCommand>, UpdateUserPasswordCommandValidator>();

            services.AddScoped<IAsyncValidator<GetEventsQuery>, GetEventsQueryValidator>();
            services.AddScoped<IAsyncValidator<GetUserByIdQuery>, GetUserByIdQueryValidator>();
            return services;
        }
    }
}
