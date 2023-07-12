using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Result;
using Application.UserAuthorizationTokens.Commands.CreateToken;
using Application.UserAuthorizationTokens.Commands.VerificationToken;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ICommandHandler<CreateTokenCommand> _tokenCreateCommandHandler;
        private readonly ICommandHandler<VerificationTokenCommand> _tokenVerificationCommandHandler;
        private readonly IConfiguration _configuration;

        private string _refreshToken;
        private string _accessToken;

        public AuthorizationController(
             ICommandHandler<CreateTokenCommand> tokenCreateCommandHandler,
             ICommandHandler<VerificationTokenCommand> tokenVerificationCommandHandler,
             IConfiguration configuration
            )
        {
            _tokenCreateCommandHandler = tokenCreateCommandHandler;
            _tokenVerificationCommandHandler = tokenVerificationCommandHandler;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizationWithToken([FromBody] long userId)
        {
            VerificationTokenCommand tokenVerificationCommand = new VerificationTokenCommand
            {
                UserId = userId,
                RefreshToken = _refreshToken
            };

            CommandResult verificationCommandResult = await _tokenVerificationCommandHandler.HandleAsync(tokenVerificationCommand);
            if (verificationCommandResult.ValidationResult.IsFail)
            {
                return BadRequest(verificationCommandResult.ValidationResult);
            }

            List<Claim> authClaims = new List<Claim>
                {
                    new Claim(nameof(userId), userId.ToString())
                };
            _accessToken = new JwtSecurityTokenHandler().WriteToken(CreateToken(authClaims));
            return Ok(verificationCommandResult.ValidationResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthorizationWithLogin([FromBody] long userId, string login, string passwordHash)
        {
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityInDay);
            CreateTokenCommand tokenCreateCommand = new CreateTokenCommand
            {
                UserId = userId,
                Login = login,
                PasswordHash = passwordHash,
                TokenValidityDays = tokenValidityInDay,
                RefreshToken = _refreshToken
            };

            CommandResult createCommandResult = await _tokenCreateCommandHandler.HandleAsync(tokenCreateCommand);
            if (createCommandResult.ValidationResult.IsFail)
            {
                return BadRequest(createCommandResult.ValidationResult);
            }

            List<Claim> authClaims = new List<Claim>
                {
                    new Claim(nameof(userId), userId.ToString())
                };
            _accessToken = new JwtSecurityTokenHandler().WriteToken(CreateToken(authClaims));
            _refreshToken = GenerateRefreshToken();

            return Ok(createCommandResult.ValidationResult);
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
