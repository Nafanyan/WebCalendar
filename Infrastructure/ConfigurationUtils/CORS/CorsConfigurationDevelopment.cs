using Newtonsoft.Json.Linq;

namespace Infrastructure.ConfigurationUtils.CORS
{
    public class CorsConfigurationDevelopment
    {
        private readonly string _configuration;

        public CorsConfigurationDevelopment()
        {
            _configuration = File.ReadAllText("appsettings.Development.json");
        }

        public string GetWithOrigins()
        {
            string result = (string)JObject.Parse(_configuration)["CORS"]["WithOrigins"]["LocalHost"];
            return result;
        }
    }
}
