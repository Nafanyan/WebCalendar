using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.UserAuthorizationTokens.Commands.UserAuthorization;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ICommandHandler<UserAuthorizationCommand> _authorizationUserCommandHandler;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
             ICommandHandler<UserAuthorizationCommand> authorizationUserCommandHandler,
            IConfiguration configuration)
        {
            _authorizationUserCommandHandler = authorizationUserCommandHandler;
            _configuration = configuration;
        }


        [HttpGet()]
        public async Task<IActionResult> CheckToken()
        {
            UserAuthorizationCommand query = new UserAuthorizationCommand
            {
                UserId = 4,
                Login = "s",
                PasswordHash = "s"
            };

            return Ok(await _authorizationUserCommandHandler.HandleAsync(query));
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
