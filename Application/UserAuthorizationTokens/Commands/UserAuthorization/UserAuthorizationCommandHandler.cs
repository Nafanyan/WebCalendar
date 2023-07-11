using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UserAuthorizationTokens.Commands.UserAuthorization
{
    public class AuthorizationUserQueryHandler : ICommandHandler<UserAuthorizationCommand>
    {
        private readonly UserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IAsyncValidator<UserAuthorizationCommand> _userAuthorizationValidator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizationUserQueryHandler(
            UserAuthorizationTokenRepository userAuthorizationTokenRepository,
            IAsyncValidator<UserAuthorizationCommand> validator,
            IUnitOfWork unitOfWork
            )
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userAuthorizationValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(UserAuthorizationCommand command)
        {
            if (! await _userAuthorizationTokenRepository.ContainsAsync(token => token.UserId == command.UserId))
            {
                await _userAuthorizationTokenRepository.AddAsync(new UserAuthorizationToken(command.RefreshToken, command.UserId));
                await _unitOfWork.CommitAsync();
                return new CommandResult(ValidationResult.Ok());
            }

            if (command.RefreshToken == (await _userAuthorizationTokenRepository.GetTokenAsync(command.UserId)).RefreshToken)
            {
                return new CommandResult(ValidationResult.Ok());
            }

            ValidationResult validationResult = await _userAuthorizationValidator.ValidationAsync(command);

            return new CommandResult(validationResult);
        }
    }
}
