using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<DeleteUserCommand> _deleteUserCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(
            IUserRepository userRepository, 
            IAsyncValidator<DeleteUserCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _deleteUserCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(DeleteUserCommand deleteUserCommand)
        {
            ValidationResult validationResult = await _deleteUserCommandValidator.ValidationAsync(deleteUserCommand);
            if (!validationResult.IsFail)
            {
                User user = _userRepository.GetByIdAsync(deleteUserCommand.Id).Result;
                _userRepository.Delete(user);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
