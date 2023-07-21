using Application.CQRSInterfaces;
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
    [Route("api/Users")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly ICommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand> _authenticateCommandHandler;
        private readonly ICommandHandler<RefreshTokenCommandDto, RefreshTokenCommand> _refreshTokenCommandHandler;

        public AuthenticationController(
            ICommandHandler<CreateUserCommand> createUserCommandHandler,
            ICommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand> authenticateCommandHandler,
            ICommandHandler<RefreshTokenCommandDto, RefreshTokenCommand> refreshTokenCommandHandler
            )
        {
            _createUserCommandHandler = createUserCommandHandler;
            _authenticateCommandHandler = authenticateCommandHandler;
            _refreshTokenCommandHandler = refreshTokenCommandHandler;
        }

        [HttpPost]
        [Route("registrate")]
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

            return Ok(commandResult);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            string refreshTokenFromCookie = Request.Cookies["RefreshToken"];
            
            RefreshTokenCommand refreshTokenCommand = new RefreshTokenCommand
            {
                RefreshToken = refreshTokenFromCookie,
            };
            CommandResult<RefreshTokenCommandDto> commandResult = await _refreshTokenCommandHandler.HandleAsync(refreshTokenCommand);

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }

            return Ok(commandResult);
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationDto authenticationDto)
        {
            AuthenticateUserCommand authenticateUserCommand = new AuthenticateUserCommand
            {
                Login = authenticationDto.Login,
                PasswordHash = authenticationDto.PasswordHash
            };
            CommandResult<AuthenticateUserCommandDto> commandResult = await _authenticateCommandHandler
                .HandleAsync(authenticateUserCommand);

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }

            return Ok(commandResult);
        }
    }
}
