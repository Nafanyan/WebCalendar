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
        private readonly IValidator<UpdateUserPasswordCommand> _updateUserPasswordCommandValidation;

        public UpdateUserPasswordCommandHandler(IUserRepository userRepository, IValidator<UpdateUserPasswordCommand> validator)
        {
            _userRepository = userRepository;
            _updateUserPasswordCommandValidation = validator;
        }

        public async Task<CommandResult> Handle(UpdateUserPasswordCommand updateUserPasswordCommand)
        {
            ValidationResult validationResult = _updateUserPasswordCommandValidation.Validation(updateUserPasswordCommand);
            if (!validationResult.IsFail)
            {
                User user = await _userRepository.GetById(updateUserPasswordCommand.Id);
                user.SetPasswordHash(updateUserPasswordCommand.NewPasswordHash);
            }
            return new CommandResult(validationResult);
        }
    }
}
