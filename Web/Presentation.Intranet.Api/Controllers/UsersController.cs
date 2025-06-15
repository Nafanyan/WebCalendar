using Application.Interfaces;
using Application.Result;
using Application.Tokens;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUserLogin;
using Application.Users.Commands.UpdateUserPassword;
using Application.Users.DTOs;
using Application.Users.Queries.GetEvents;
using Application.Users.Queries.GetUserById;
using Infrastructure.JwtAuthorizations;
using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route( "Api/[controller]" )]
    [JwtAuthorization]
    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<DeleteUserCommand> _deleteUserCommandHandler;
        private readonly ICommandHandler<UpdateUserLoginCommand> _updateUserLoginCommandHandler;
        private readonly ICommandHandler<UpdateUserPasswordCommand> _updateUserPasswordCommandHandler;
        private readonly IQueryHandler<IReadOnlyList<GetEventsQueryDto>, GetEventsQuery> _getEventQueryHandler;
        private readonly IQueryHandler<GetUserByIdQueryDto, GetUserByIdQuery> _getUserByIdQueryHandler;

        public UsersController(
            ICommandHandler<DeleteUserCommand> deleteUserCommandHandler,
            ICommandHandler<UpdateUserLoginCommand> updateUserLoginCommandHandler,
            ICommandHandler<UpdateUserPasswordCommand> updateUserPasswordCommandHandler,
            IQueryHandler<IReadOnlyList<GetEventsQueryDto>, GetEventsQuery> getEventQueryHandler,
            IQueryHandler<GetUserByIdQueryDto, GetUserByIdQuery> getUserByIdQueryHandler,
            ITokenConfiguration configuration
            )
        {
            _deleteUserCommandHandler = deleteUserCommandHandler;
            _updateUserLoginCommandHandler = updateUserLoginCommandHandler;
            _updateUserPasswordCommandHandler = updateUserPasswordCommandHandler;
            _getEventQueryHandler = getEventQueryHandler;
            _getUserByIdQueryHandler = getUserByIdQueryHandler;
        }

        [HttpGet( "{userId}" )]
        public async Task<IActionResult> GetUserById( [FromRoute] long userId )
        {
            GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery
            {
                Id = userId
            };
            QueryResult<GetUserByIdQueryDto> queryResult = await _getUserByIdQueryHandler.HandleAsync( getUserByIdQuery );

            if( queryResult.ValidationResult.IsFail )
            {
                return BadRequest( queryResult );
            }
            return Ok( queryResult );
        }

        [HttpGet( "{userId}/Event" )]
        public async Task<IActionResult> GetEvents( [FromRoute] long userId, DateTime startEvent, DateTime endEvent )
        {
            GetEventsQuery getEventsQuery = new GetEventsQuery
            {
                UserId = userId,
                StartEvent = startEvent,
                EndEvent = endEvent
            };
            QueryResult<IReadOnlyList<GetEventsQueryDto>> queryResult = await _getEventQueryHandler.HandleAsync( getEventsQuery );

            if( queryResult.ValidationResult.IsFail )
            {
                return BadRequest( queryResult );
            }
            return Ok( queryResult.ObjResult );
        }

        [HttpDelete( "{userId}" )]
        public async Task<IActionResult> Delete( [FromRoute] long userId, [FromBody] DeleteUserDto deleteUserDto )
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand
            {
                Id = userId,
                PasswordHash = deleteUserDto.PasswordHash
            };
            CommandResult commandResult = await _deleteUserCommandHandler.HandleAsync( deleteUserCommand );

            if( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok();
        }

        [HttpPut( "Update-Login/{userId}" )]
        public async Task<IActionResult> UpdateLogin( [FromRoute] long userId, [FromBody] UpdateUserLoginDto updateUserLoginDto )
        {
            UpdateUserLoginCommand updateUserLoginCommand = new UpdateUserLoginCommand
            {
                Id = userId,
                Login = updateUserLoginDto.Login,
                PasswordHash = updateUserLoginDto.PasswordHash
            };
            CommandResult commandResult = await _updateUserLoginCommandHandler.HandleAsync( updateUserLoginCommand );

            if( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok();
        }

        [HttpPut( "Update-Password/{userId}" )]
        public async Task<IActionResult> UpdatePassword( [FromRoute] long userId, [FromBody] UpdateUserPasswordDto updateUserPasswordDto )
        {
            UpdateUserPasswordCommand updateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = userId,
                OldPasswordHash = updateUserPasswordDto.OldPasswordHash,
                NewPasswordHash = updateUserPasswordDto.NewPasswordHash
            };
            CommandResult commandResult = await _updateUserPasswordCommandHandler.HandleAsync( updateUserPasswordCommand );

            if( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok();
        }
    }
}
