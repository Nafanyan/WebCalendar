using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UserAuthorizationTokens.Commands.RegistrationUser
{
    public class RegistrationUserCommandHandler : ICommandHandler<RegistrationUserCommand>
    {
        private readonly IUserAuthorizationRepository _userAuthorizationRepository;
        private readonly IAsyncValidator<RegistrationUserCommand> _registrationUserCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationUserCommandHandler(
            IUserAuthorizationRepository userAuthorizationRepository,
            IAsyncValidator<RegistrationUserCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _userAuthorizationRepository = userAuthorizationRepository;
            _registrationUserCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(RegistrationUserCommand userRegistrationCommand)
        {
            ValidationResult validationResult = await _registrationUserCommandValidator.ValidationAsync(userRegistrationCommand);

            if (!validationResult.IsFail)
            {
                //Create token
                //Add token in db 
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
