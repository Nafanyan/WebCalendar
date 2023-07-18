using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Result;
using Application.UserAuthorizationTokens.Commands.AuthenticateUser;
using Application.UserAuthorizationTokens.Commands.RefreshToken;
using Application.Users.Commands.CreateUser;
using Presentation.Intranet.Api.Dtos.UserDtos;
using Presentation.Intranet.Api.Dtos.AuthenticationDtos;
using Application.UserAuthorizationTokens.Commands;
using Application.UserAuthorizationTokens.DTOs;
using Application.Users.DTOs;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly IAuthorizationCommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand> _authorizationCommandHandler;
        private readonly IAuthorizationCommandHandler<RefreshTokenCommandDto, RefreshTokenCommand> _refreshTokenCommandHandler;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            ICommandHandler<CreateUserCommand> createUserCommandHandler,
            IAuthorizationCommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand> authorizationCommandHandler,
            IAuthorizationCommandHandler<RefreshTokenCommandDto, RefreshTokenCommand> refreshTokenCommandHandler,
            IConfiguration configuration
            )
        {
            _createUserCommandHandler = createUserCommandHandler;
            _authorizationCommandHandler = authorizationCommandHandler;
            _refreshTokenCommandHandler = refreshTokenCommandHandler;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Registrate")]
        public async Task<IActionResult> Registrate([FromBody] RegistrateUserDto registrateUserDto)
        {
            CreateUserCommand createUserCommand = new CreateUserCommand
            {
                Login = registrateUserDto.Login,
                PasswordHash = registrateUserDto.PasswordHash
            };
            CommandResult commandResult = await _createUserCommandHandler.HandleAsync(createUserCommand);

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }

            return Ok();
        }

        [HttpPost("Refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            DateTime newRefreshTokenExpiryDate = DateTime.Now.AddDays(int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]));
            string refreshTokenFromCookie = Request.Cookies["RefreshToken"];
            var s = HttpContext;

            RefreshTokenCommand refreshTokenCommand = new RefreshTokenCommand
            {
                RefreshToken = refreshTokenFromCookie,
                NewRefreshTokenExpiryDate = newRefreshTokenExpiryDate
            };
            AuthorizationCommandResult<RefreshTokenCommandDto> commandResult = await _refreshTokenCommandHandler.HandleAsync(refreshTokenCommand);

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }

            //Response.Cookies.Append("RefreshToken", commandResult.ObjResult.RefreshToken, new CookieOptions
            //{
            //    Expires = newRefreshTokenExpiryDate
            //});

            return Ok(commandResult);
        }

        [HttpPost("Authentication")]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationDto authenticationDto)
        {
            DateTime newRefreshTokenExpiryDate = DateTime.Now.AddDays(int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]));

            AuthenticateUserCommand authenticateUserCommand = new AuthenticateUserCommand
            {
                Login = authenticationDto.Login,
                PasswordHash = authenticationDto.PasswordHash,
                NewRefreshTokenExpiryDate = newRefreshTokenExpiryDate
            };
            AuthorizationCommandResult<AuthenticateUserCommandDto> commandResult = await _authorizationCommandHandler.HandleAsync(authenticateUserCommand);

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }

            //Response.Cookies.Delete("RefreshToken");
            //Response.Cookies.Append("RefreshToken", commandResult.ObjResult.RefreshToken, new CookieOptions
            //{
            //    Expires = newRefreshTokenExpiryDate
            //});

            return Ok(commandResult);
        }
    }
}
