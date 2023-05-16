using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly DeleteUserCommandValidation _deleteUserCommandValidation;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _deleteUserCommandValidation = new DeleteUserCommandValidation(userRepository);
        }

        public async Task<ResultCommand> Handle(DeleteUserCommand deleteUserCommand)
        {
            string msg = _deleteUserCommandValidation.Validation(deleteUserCommand);
            if (msg == "Ok")
            {
                User user = _userRepository.GetById(deleteUserCommand.Id).Result;
                await _userRepository.Delete(user);
            }
            return new ResultCommand(msg);
        }
    }
}
