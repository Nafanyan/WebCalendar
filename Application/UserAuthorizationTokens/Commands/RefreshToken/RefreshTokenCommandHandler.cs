using Application.UserAuthorizationTokens.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UserAuthorizationTokens.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IAuthorizationCommandHandler<RefreshTokenCommandDto, RefreshTokenCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IAsyncValidator<RefreshTokenCommand> _userAuthorizationTokenValidator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenCreator _tokenCreator;

        public RefreshTokenCommandHandler(
            IUserAuthorizationTokenRepository userAuthorizationTokenRepository,
            IAsyncValidator<RefreshTokenCommand> validator,
            IUnitOfWork unitOfWork,
            ITokenCreator tokenCreator)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userAuthorizationTokenValidator = validator;
            _unitOfWork = unitOfWork;
            _tokenCreator = tokenCreator;
        }

        public async Task<AuthorizationCommandResult<RefreshTokenCommandDto>> HandleAsync(RefreshTokenCommand command)
        {
            ValidationResult validationResult = await _userAuthorizationTokenValidator.ValidationAsync(command);

            if (validationResult.IsFail)
            {
                return new AuthorizationCommandResult<RefreshTokenCommandDto>(validationResult);
            }

            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetByRefreshTokenAsync(command.RefreshToken);
            _userAuthorizationTokenRepository.Delete(token);

            string accessToken = _tokenCreator.CreateAccessToken(token.UserId);
            string refreshToken = _tokenCreator.CreateRefreshToken();

            UserAuthorizationToken newToken = new UserAuthorizationToken(
                token.UserId,
                refreshToken,
                command.NewRefreshTokenExpiryDate);
            _userAuthorizationTokenRepository.Add(newToken);

            await _unitOfWork.CommitAsync();

            RefreshTokenCommandDto refreshTokenCommandResult = new RefreshTokenCommandDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return new AuthorizationCommandResult<RefreshTokenCommandDto>(refreshTokenCommandResult);
        }
    }
}
