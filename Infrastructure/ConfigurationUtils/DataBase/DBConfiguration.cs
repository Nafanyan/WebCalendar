using Newtonsoft.Json.Linq;

namespace Infrastructure.ConfigurationUtils.DataBase
{
    public class DBConfiguration : IDBConfiguration
    {
        private readonly string _configuration;
        public DBConfiguration()
        {
            _configuration = File.ReadAllText("../../Infrastructure/appsettings.json");
        }
        public string GetConnectionString(string nameDB)
        {
            string result = (string)JObject.Parse(_configuration)["ConnectionStrings"][nameDB];
            return result;
        }
    }
}
