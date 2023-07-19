using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Tokens.CreateToken
{
    public class TokenCreator
    {
        private readonly ITokenConfiguration _tokenConfiguration;

        public TokenCreator(ITokenConfiguration configuration)
        {
            _tokenConfiguration = configuration;
        }

        public string CreateAccessToken(long userId)
        {
            List<Claim> authClaims = new List<Claim>
            {
                new Claim(nameof(userId), userId.ToString())
            };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.GetSecret()));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(_tokenConfiguration.GetAccessTokenValidityInMinutes()),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
