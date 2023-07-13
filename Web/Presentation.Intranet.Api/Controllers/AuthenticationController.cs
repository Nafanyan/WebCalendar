using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Result;
using Application.UserAuthorizationTokens.Commands.AuthenticateUser;
using Application.UserAuthorizationTokens.Commands.RefreshToken;
using Application.Users.Commands.CreateUser;
using Presentation.Intranet.Api.Dtos.UserDtos;
using Presentation.Intranet.Api.Mappers.UserMappers;
using Presentation.Intranet.Api.Dtos.AuthenticationDtos;
using Presentation.Intranet.Api.Mappers.AuthenticationMappers;
using Application.UserAuthorizationTokens.Commands;
using Application.UserAuthorizationTokens.DTOs;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly IAuthorizationCommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand> _authorizationCommandHandler;
        private readonly IAuthorizationCommandHandler<RefreshTokenCommandDto, RefreshTokenCommand> _verifyTokenCommandHandler;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            ICommandHandler<CreateUserCommand> createUserCommandHandler,
            IAuthorizationCommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand> authorizationCommandHandler,
            IAuthorizationCommandHandler<RefreshTokenCommandDto, RefreshTokenCommand> verifyTokenCommandHandler,
            IConfiguration configuration
            )
        {
            _createUserCommandHandler = createUserCommandHandler;
            _authorizationCommandHandler = authorizationCommandHandler;
            _verifyTokenCommandHandler = verifyTokenCommandHandler;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registrate")]
        public async Task<IActionResult> Registrate([FromBody] CreateUserDto createUserDto)
        {
            CommandResult commandResult = await _createUserCommandHandler.HandleAsync(createUserDto.Map());

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }

            return Ok(commandResult);
        }

        [HttpPost("authentication-with-token")]
        public async Task<IActionResult> AuthenticationWithToken([FromBody] AuthenticationWithTokenDto authenticationWithTokenDto)
        {
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityInDay);
            DateTime refreshTokenExpiryDate = DateTime.Now.AddDays(tokenValidityInDay);

            RefreshTokenCommand verifyTokenCommand = new RefreshTokenCommand
            {
                RefreshToken = authenticationWithTokenDto.RefreshToken,
                RefreshTokenExpiryDate = refreshTokenExpiryDate
            };

            AuthorizationCommandResult<RefreshTokenCommandDto> commandResult = await _verifyTokenCommandHandler.HandleAsync(verifyTokenCommand);
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }

            Response.Cookies.Append("RefreshToken", commandResult.ObjResult.RefreshToken, new CookieOptions
            {
                Expires = refreshTokenExpiryDate
            });

            return Ok(commandResult);
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> AuthenticationWithLogin([FromBody] AuthenticationWithLoginDto authenticationWithLoginDto)
        {
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityInDay);
            DateTime refreshTokenExpiryDate = DateTime.Now.AddDays(tokenValidityInDay);

            AuthorizationCommandResult<AuthenticateUserCommandDto> commandResult = await _authorizationCommandHandler.HandleAsync(
                authenticationWithLoginDto.Map(refreshTokenExpiryDate));

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }

            Response.Cookies.Append("RefreshToken", commandResult.ObjResult.RefreshToken, new CookieOptions
            {
                Expires = refreshTokenExpiryDate
            });

            return Ok(commandResult);
        }
    }
}
