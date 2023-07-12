using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UserPasswordUpdateCommandHandler : ICommandHandler<UserPasswordUpdateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<UserPasswordUpdateCommand> _updateUserPasswordCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public UserPasswordUpdateCommandHandler(
            IUserRepository userRepository, 
            IAsyncValidator<UserPasswordUpdateCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _updateUserPasswordCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(UserPasswordUpdateCommand updateUserPasswordCommand)
        {
            ValidationResult validationResult = await _updateUserPasswordCommandValidator.ValidationAsync(updateUserPasswordCommand);
            if (!validationResult.IsFail)
            {
                User user = await _userRepository.GetByIdAsync(updateUserPasswordCommand.Id);
                user.SetPasswordHash(updateUserPasswordCommand.NewPasswordHash);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
