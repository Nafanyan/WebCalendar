using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.Users.Commands.DeleteUser
{
    public class UserDeleteCommandHandler : ICommandHandler<UserDeleteCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<UserDeleteCommand> _deleteUserCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public UserDeleteCommandHandler(
            IUserRepository userRepository, 
            IAsyncValidator<UserDeleteCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _deleteUserCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(UserDeleteCommand deleteUserCommand)
        {
            ValidationResult validationResult = await _deleteUserCommandValidator.ValidationAsync(deleteUserCommand);
            if (!validationResult.IsFail)
            {
                User user = _userRepository.GetByIdAsync(deleteUserCommand.Id).Result;
                await _userRepository.DeleteAsync(user);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
