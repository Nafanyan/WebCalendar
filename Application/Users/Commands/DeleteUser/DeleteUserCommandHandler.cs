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
        private readonly IValidator<DeleteUserCommand> _deleteUserCommandValidation;

        public DeleteUserCommandHandler(IUserRepository userRepository, IValidator<DeleteUserCommand> validator)
        {
            _userRepository = userRepository;
            _deleteUserCommandValidation = validator;
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
