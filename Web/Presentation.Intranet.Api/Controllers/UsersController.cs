using Application.Events.Queries.GetEvent;
using Application.Interfaces;
using Application.Result;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUserLogin;
using Application.Users.Commands.UpdateUserPassword;
using Application.Users.Queries.GetEvents;
using Application.Users.Queries.GetUserById;
using Domain.Entities;
using Domain.UnitOfWork;
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
        private readonly IQueryHandler<IReadOnlyList<Event>, GetEventsQuery> _getEventQueryHandler;
        private readonly IQueryHandler<User, GetUserByIdQuery> _getUserByIdQueryHandler;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(ICommandHandler<CreateUserCommand> createUserCommandHandler,
            ICommandHandler<DeleteUserCommand> deleteUserCommandHandler,
            ICommandHandler<UpdateUserLoginCommand> updateUserLoginCommandHandler,
            ICommandHandler<UpdateUserPasswordCommand> updateUserPasswordCommandHandler,
            IQueryHandler<IReadOnlyList<Event>, GetEventsQuery> getEventQueryHandler,
            IQueryHandler<User, GetUserByIdQuery> getUserByIdQueryHandler,
            IUnitOfWork unitOfWork
            )
        {
            _createUserCommandHandler = createUserCommandHandler;
            _deleteUserCommandHandler = deleteUserCommandHandler;
            _updateUserLoginCommandHandler = updateUserLoginCommandHandler;
            _updateUserPasswordCommandHandler = updateUserPasswordCommandHandler;
            _getEventQueryHandler = getEventQueryHandler;
            _getUserByIdQueryHandler = getUserByIdQueryHandler;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery
            {
                Id = id
            };
            QueryResult<User> queryResult = await _getUserByIdQueryHandler.HandleAsync(getUserByIdQuery);

            if (queryResult.ValidationResult.IsFail)
            {
                return BadRequest(queryResult);
            }
            return Ok(queryResult);
        }

        [HttpGet("{id}/Event")]
        public async Task<IActionResult> GetEvents([FromRoute] long id)
        {
            GetEventsQuery getEventsQuery = new GetEventsQuery
            {
                UserId = id
            };
            QueryResult<IReadOnlyList<Event>> queryResult = await _getEventQueryHandler.HandleAsync(getEventsQuery);
            
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
            await _unitOfWork.CommitAsync();

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
            await _unitOfWork.CommitAsync();

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
