using Application.Interfaces;
using Application.JWT.Commands.CreateJWT;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.JWTs
{
    public static class JWTBindings
    {
        public static IServiceCollection AddJWTBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateJWTCommand>, CreateJWTCommandHandler>();

            services.AddScoped<IAsyncValidator<CreateJWTCommand>, CreateJWTCommandValidator>();
            return services;
        }
    }
}
