using Application.Tokens;

namespace Presentation.Intranet.Api.Configurations
{
    public class TokenConfiguration : ITokenConfiguration
    {
        private readonly IConfiguration _configuration;

        public TokenConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetAccessTokenValidityInMinutes()
        {
            return _configuration["JWT:TokenValidityInMinutes"];
        }

        public string GetRefreshTokenValidityInDays()
        {
            return _configuration["JWT:RefreshTokenValidityInDays"];
        }

        public string GetSecret()
        {
            return _configuration["JWT:Secret"];
        }
    }
}
