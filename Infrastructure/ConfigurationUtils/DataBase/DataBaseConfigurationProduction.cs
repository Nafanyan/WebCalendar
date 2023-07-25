using Newtonsoft.Json.Linq;

namespace Infrastructure.ConfigurationUtils.DataBase
{
    public class DataBaseConfigurationProduction
    {
        private readonly string _configuration;

        public DataBaseConfigurationProduction()
        {
            _configuration = File.ReadAllText("appsettings.Production.json");
        }

        public string GetConnectionString()
        {
            string result = (string)JObject.Parse(_configuration)["ConnectionString"];
            return result;
        }
    }
}
