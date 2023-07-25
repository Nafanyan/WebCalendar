using Newtonsoft.Json.Linq;

namespace Infrastructure.ConfigurationUtils.DataBase
{
    public class DataBaseConfigurationDevelopment
    {
        private readonly string _configuration;

        public DataBaseConfigurationDevelopment()
        {
            _configuration = File.ReadAllText("appsettings.Development.json");
        }

        public string GetConnectionString()
        {
            string result = (string)JObject.Parse(_configuration)["ConnectionString"];
            return result;
        }
    }
}
