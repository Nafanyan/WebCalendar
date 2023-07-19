using Application.Tokens;
using Newtonsoft.Json.Linq;

namespace Infrastructure.ConfigurationUtils.Token
{
    public class TokenConfiguration : ITokenConfiguration
    {
        private readonly string _configuration;

        public TokenConfiguration()
        {
            _configuration = File.ReadAllText("../../Infrastructure/appsettings.json");
        }

        public int GetAccessTokenValidityInMinutes()
        {
            int result = (int)JObject.Parse(_configuration)["JWT"]["TokenValidityInMinutes"];
            return result;
        }

        public int GetRefreshTokenValidityInDays()
        {
            int result = (int)JObject.Parse(_configuration)["JWT"]["RefreshTokenValidityInDays"];
            return result;
        }

        public string GetSecret()
        {
            string result = (string)JObject.Parse(_configuration)["JWT"]["Secret"];
            return result;
        }
    }
}
