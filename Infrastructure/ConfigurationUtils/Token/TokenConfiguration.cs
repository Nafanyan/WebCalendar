using Application.Tokens;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.ConfigurationUtils.Token
{
    public class TokenConfiguration : ITokenConfiguration
    {
        private readonly IConfiguration _configuration;

        public TokenConfiguration( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public int GetAccessTokenValidityInMinutes()
        {
            string result = _configuration["JWT:TokenValidityInMinutes"];
            return int.Parse(result);
        }

        public int GetRefreshTokenValidityInDays()
        {
            string result = _configuration["JWT:RefreshTokenValidityInDays"];
            return int.Parse(result);
        }

        public string GetSecret()
        {
            string result = _configuration["JWT:Secret"];
            return result;
        }
    }
}
