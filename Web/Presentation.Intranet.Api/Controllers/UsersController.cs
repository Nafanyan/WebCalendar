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
using Domain.Repositories;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.Dtos.UserDtos;
using Presentation.Intranet.Api.Mappers.UserMappers;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly ICommandHandler<DeleteUserCommand> _deleteUserCommandHandler;
        private readonly ICommandHandler<UpdateUserLoginCommand> _updateUserLoginCommandHandler;
        private readonly ICommandHandler<UpdateUserPasswordCommand> _updateUserPasswordCommandHandler;
        private readonly IQueryHandler<IReadOnlyList<Event>, GetEventsQuery> _getEventQueryHandler;
        private readonly IQueryHandler<User, GetUserByIdQuery> _getUserByIdQueryHandler;

        public UsersController(ICommandHandler<CreateUserCommand> createUserCommandHandler,
            ICommandHandler<DeleteUserCommand> deleteUserCommandHandler,
            ICommandHandler<UpdateUserLoginCommand> updateUserLoginCommandHandler,
            ICommandHandler<UpdateUserPasswordCommand> updateUserPasswordCommandHandler,
            IQueryHandler<IReadOnlyList<Event>, GetEventsQuery> getEventQueryHandler,
            IQueryHandler<User, GetUserByIdQuery> getUserByIdQueryHandler
            )
        {
            _createUserCommandHandler = createUserCommandHandler;
            _deleteUserCommandHandler = deleteUserCommandHandler;
            _updateUserLoginCommandHandler = updateUserLoginCommandHandler;
            _updateUserPasswordCommandHandler = updateUserPasswordCommandHandler;
            _getEventQueryHandler = getEventQueryHandler;
            _getUserByIdQueryHandler = getUserByIdQueryHandler;
        }

        [HttpGet("[controller]/{idUser}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]GetUserByIdDto getUserByIdDto)
        {
            QueryResult<User> queryResult = await _getUserByIdQueryHandler.HandleAsync(getUserByIdDto.Map());
            return Ok(queryResult);
        }
        [HttpPost("[controller]")]
        public async Task<IActionResult> AddAsync(CreateUserDto createUserDto)
        {
            CommandResult commandResult = await _createUserCommandHandler.HandleAsync(createUserDto.Map());
            return Ok(commandResult);
        }
    }
}
