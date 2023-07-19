using Application.Tokens;
using Infrastructure.ConfigurationUtils.DataBase;
using Infrastructure.ConfigurationUtils.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ConfigurationUtils
{
    public static class ConfigurationInterfacesBindings
    {
        public static IServiceCollection AddConfigurationUtilsBindings(this IServiceCollection services)
        {
            services.AddScoped<IDBConfiguration, DBConfiguration>();
            services.AddScoped<ITokenConfiguration, TokenConfiguration>();
            return services;
        }
    }
}
