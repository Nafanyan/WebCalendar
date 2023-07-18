using Application.Tokens;
using Presentation.Intranet.Api.Configurations;

namespace Presentation.Intranet.Api.ConfigurationUtils
{
    public static class ConfigurationUtilsBindings
    {
        public static IServiceCollection AddConfigurationUtilsBindings(this IServiceCollection services)
        {
            services.AddScoped<ITokenConfiguration, TokenConfiguration>();
            return services;
        }
    }
}
