using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Infrastructure.ConfigurationUtils.CORS
{
    public class CorsConfigurationProduction
    {
        private readonly string _configuration;
        public CorsConfigurationProduction()
        {
            _configuration = File.ReadAllText("../../Infrastructure/appsettings.Production.json");
        }
        public string GetWithOrigins()
        {
            string result = (string)JObject.Parse(_configuration)["CORS"]["WithOrigins"];
            return result;
        }
    }
}
