using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Tokens.CreateToken
{
    public class TokenCreator
    {
        private readonly ITokenConfiguration _configuration;

        public TokenCreator(ITokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(long userId)
        {
            List<Claim> authClaims = new List<Claim>
            {
                new Claim(nameof(userId), userId.ToString())
            };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSecret()));
            _ = int.TryParse(_configuration.GetAccessTokenValidityInMinutes(), out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
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
