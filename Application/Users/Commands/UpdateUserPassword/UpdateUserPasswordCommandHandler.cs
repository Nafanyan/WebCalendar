using Application.CQRSInterfaces;
using Application.Result;
using Application.Validation;
using Application.Entities;
using Application.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<UpdateUserPasswordCommand> _updateUserPasswordCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserPasswordCommandHandler(
            IUserRepository userRepository, 
            IAsyncValidator<UpdateUserPasswordCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _updateUserPasswordCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(UpdateUserPasswordCommand updateUserPasswordCommand)
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
