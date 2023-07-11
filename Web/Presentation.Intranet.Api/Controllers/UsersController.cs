using Application.Interfaces;
using Application.Result;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUserLogin;
using Application.Users.Commands.UpdateUserPassword;
using Application.Users.DTOs;
using Application.Users.Queries.EventsQuery;
using Application.Users.Queries.QueryUserById;
using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.Dtos.UserDtos;
using Presentation.Intranet.Api.Mappers.UserMappers;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<UserCreateCommand> _createUserCommandHandler;
        private readonly ICommandHandler<UserDeleteCommand> _deleteUserCommandHandler;
        private readonly ICommandHandler<UserLoginUpdateCommand> _updateUserLoginCommandHandler;
        private readonly ICommandHandler<UserPasswordUpdateCommand> _updateUserPasswordCommandHandler;
        private readonly IQueryHandler<IReadOnlyList<EventsQueryDto>, EventsQuery> _eventQueryHandler;
        private readonly IQueryHandler<UserQueryByIdDto, UserQueryById> _userByIdQueryHandler;

        public UsersController(
            ICommandHandler<UserCreateCommand> createUserCommandHandler,
            ICommandHandler<UserDeleteCommand> deleteUserCommandHandler,
            ICommandHandler<UserLoginUpdateCommand> updateUserLoginCommandHandler,
            ICommandHandler<UserPasswordUpdateCommand> updateUserPasswordCommandHandler,
            IQueryHandler<IReadOnlyList<EventsQueryDto>, EventsQuery> eventQueryHandler,
            IQueryHandler<UserQueryByIdDto, UserQueryById> userByIdQueryHandler)
        {
            _createUserCommandHandler = createUserCommandHandler;
            _deleteUserCommandHandler = deleteUserCommandHandler;
            _updateUserLoginCommandHandler = updateUserLoginCommandHandler;
            _updateUserPasswordCommandHandler = updateUserPasswordCommandHandler;
            _eventQueryHandler = eventQueryHandler;
            _userByIdQueryHandler = userByIdQueryHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            UserQueryById getUserByIdQuery = new UserQueryById
            {
                Id = id
            };
            QueryResult<UserQueryByIdDto> queryResult = await _userByIdQueryHandler.HandleAsync(getUserByIdQuery);

            if (queryResult.ValidationResult.IsFail)
            {
                return BadRequest(queryResult);
            }
            return Ok(queryResult);
        }

        [HttpGet("{id}/Event")]
        public async Task<IActionResult> GetEvents([FromRoute] long id, DateTime startEvent, DateTime endEvent)
        {
            EventsQuery getEventsQuery = new EventsQuery
            {
                UserId = id,
                StartEvent = startEvent,
                EndEvent = endEvent
            };
            QueryResult<IReadOnlyList<EventsQueryDto>> queryResult = await _eventQueryHandler.HandleAsync(getEventsQuery);
            
            if (queryResult.ValidationResult.IsFail)
            {
                return BadRequest(queryResult);
            }
            return Ok(queryResult.ObjResult);
        }

        [HttpPost()]
        public async Task<IActionResult> AddAsync([FromBody] CreateUserDto createUserDto)
        {
            CommandResult commandResult = await _createUserCommandHandler.HandleAsync(createUserDto.Map());

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }
            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteUserDto deleteUserDto)
        {
            CommandResult commandResult = await _deleteUserCommandHandler.HandleAsync(deleteUserDto.Map());

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }
            return Ok();
        }

        [HttpPut("Update-login")]
        public async Task<IActionResult> UpdateLogin([FromBody] UpdateUserLoginDto updateUserLoginDto)
        {
            CommandResult commandResult = await _updateUserLoginCommandHandler.HandleAsync(updateUserLoginDto.Map());

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }
            return Ok();
        }

        [HttpPut("Update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordDto updateUserPasswordDto)
        {
            CommandResult commandResult = await _updateUserPasswordCommandHandler.HandleAsync(updateUserPasswordDto.Map());
            
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }
            return Ok();
        }
    }
}
