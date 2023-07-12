using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UserAuthorizationTokens.Commands.CreateToken
{
    public class TokenCreateCommandHandler : ICommandHandler<TokenCreateCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IAsyncValidator<TokenCreateCommand> _userAuthorizationTokenValidator;
        private readonly IUnitOfWork _unitOfWork;

        public TokenCreateCommandHandler(
            IUserAuthorizationTokenRepository userAuthorizationTokenRepository,
            IAsyncValidator<TokenCreateCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userAuthorizationTokenValidator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResult> HandleAsync(TokenCreateCommand command)
        {
            ValidationResult validationResult = await _userAuthorizationTokenValidator.ValidationAsync(command);

            if (!validationResult.IsFail)
            {
                UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetTokenByUserIdAsync(command.UserId);
                UserAuthorizationToken newToken = new UserAuthorizationToken(
                    command.RefreshToken,
                    command.UserId,
                    DateTime.Now.AddDays(command.TokenValidityDays));

                if (token is null)
                {
                    await _userAuthorizationTokenRepository.AddAsync(newToken);
                }
                else
                {
                    token = newToken;
                }
                await _unitOfWork.CommitAsync();
            }

            return new CommandResult(validationResult);
        }
    }
}
