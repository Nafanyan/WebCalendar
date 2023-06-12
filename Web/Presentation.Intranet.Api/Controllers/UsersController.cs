using Application.Events.Queries.GetEvent;
using Application.Interfaces;
using Application.Result;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUserLogin;
using Application.Users.Commands.UpdateUserPassword;
using Application.Users.DTOs;
using Application.Users.Queries.GetEvents;
using Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.Dtos.UserDtos;
using Presentation.Intranet.Api.Mappers.UserMappers;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly ICommandHandler<DeleteUserCommand> _deleteUserCommandHandler;
        private readonly ICommandHandler<UpdateUserLoginCommand> _updateUserLoginCommandHandler;
        private readonly ICommandHandler<UpdateUserPasswordCommand> _updateUserPasswordCommandHandler;
        private readonly IQueryHandler<GetEventsQueryDto, GetEventsQuery> _getEventQueryHandler;
        private readonly IQueryHandler<GetUserByIdQueryDto, GetUserByIdQuery> _getUserByIdQueryHandler;

        public UsersController(
            ICommandHandler<CreateUserCommand> createUserCommandHandler,
            ICommandHandler<DeleteUserCommand> deleteUserCommandHandler,
            ICommandHandler<UpdateUserLoginCommand> updateUserLoginCommandHandler,
            ICommandHandler<UpdateUserPasswordCommand> updateUserPasswordCommandHandler,
            IQueryHandler<GetEventsQueryDto, GetEventsQuery> getEventQueryHandler,
            IQueryHandler<GetUserByIdQueryDto, GetUserByIdQuery> getUserByIdQueryHandler)
        {
            _createUserCommandHandler = createUserCommandHandler;
            _deleteUserCommandHandler = deleteUserCommandHandler;
            _updateUserLoginCommandHandler = updateUserLoginCommandHandler;
            _updateUserPasswordCommandHandler = updateUserPasswordCommandHandler;
            _getEventQueryHandler = getEventQueryHandler;
            _getUserByIdQueryHandler = getUserByIdQueryHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery
            {
                Id = id
            };
            QueryResult<GetUserByIdQueryDto> queryResult = await _getUserByIdQueryHandler.HandleAsync(getUserByIdQuery);

            if (queryResult.ValidationResult.IsFail)
            {
                return BadRequest(queryResult);
            }
            return Ok(queryResult);
        }

        [HttpGet("{id}/Event")]
        public async Task<IActionResult> GetEvents([FromRoute] long id, DateTime startEvent, DateTime endEvent)
        {
            GetEventsQuery getEventsQuery = new GetEventsQuery
            {
                UserId = id,
                StartEvent = startEvent,
                EndEvent = endEvent
            };
            QueryResult<GetEventsQueryDto> queryResult = await _getEventQueryHandler.HandleAsync(getEventsQuery);
            
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
