using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<UpdateUserPasswordCommand> _updateUserPasswordCommandValidator;

        public UpdateUserPasswordCommandHandler(IUserRepository userRepository, IAsyncValidator<UpdateUserPasswordCommand> validator)
        {
            _userRepository = userRepository;
            _updateUserPasswordCommandValidator = validator;
        }

        public async Task<CommandResult> Handle(UpdateUserPasswordCommand updateUserPasswordCommand)
        {
            ValidationResult validationResult = await _updateUserPasswordCommandValidator.Validation(updateUserPasswordCommand);
            if (!validationResult.IsFail)
            {
                User user = await _userRepository.GetById(updateUserPasswordCommand.Id);
                user.SetPasswordHash(updateUserPasswordCommand.NewPasswordHash);
            }
            return new CommandResult(validationResult);
        }
    }
}
