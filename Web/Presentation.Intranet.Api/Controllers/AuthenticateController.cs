using Application.Interfaces;
using Application.UserAuthorizationTokens.DTOs;
using Application.UserAuthorizationTokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IQueryHandler<GetTokenQueryDto, UserAuthorizationQuery> _authorizationUserQueryHandler;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            IQueryHandler<GetTokenQueryDto, UserAuthorizationQuery> authorizationUserQueryHandler,
            IConfiguration configuration)
        {
            _authorizationUserQueryHandler = authorizationUserQueryHandler;
            _configuration = configuration;
        }


        [HttpGet()]
        public async Task<IActionResult> CheckToken()
        {
            UserAuthorizationQuery query = new UserAuthorizationQuery
            {
                UserId = 4,
                Login = "s",
                PasswordHash = "s"
            };

            return Ok(await _authorizationUserQueryHandler.HandleAsync(query));
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
