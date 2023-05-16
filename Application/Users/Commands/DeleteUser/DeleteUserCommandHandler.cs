using Application.Interfaces;
using Application.Result;
using Application.Validation;
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

        public async Task<CommandResult> Handle(DeleteUserCommand deleteUserCommand)
        {
            ValidationResult validationResult = _deleteUserCommandValidation.Validation(deleteUserCommand);
            if (!validationResult.IsFail)
            {
                User user = _userRepository.GetById(deleteUserCommand.Id).Result;
                await _userRepository.Delete(user);
            }
            return new CommandResult(validationResult);
        }
    }
}
