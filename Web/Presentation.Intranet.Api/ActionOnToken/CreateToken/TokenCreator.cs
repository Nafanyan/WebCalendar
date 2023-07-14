using Application.UserAuthorizationTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Intranet.Api.ActionOnToken.CreateToken
{
    public class TokenCreator : ITokenCreator
    {
        private readonly IConfiguration _configuration;

        public TokenCreator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(long userId)
        {
            List<Claim> authClaims = new List<Claim>
            {
                new Claim(nameof(userId), userId.ToString())
            };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes).AddHours(DateTime.Now.Hour - DateTime.UtcNow.Hour),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
