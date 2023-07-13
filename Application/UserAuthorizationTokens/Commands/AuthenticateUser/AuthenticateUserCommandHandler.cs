using Application.UserAuthorizationTokens.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UserAuthorizationTokens.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : IAuthorizationCommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<AuthenticateUserCommand> _userAuthorizationTokenValidator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenCreator _tokenCreator;

        public AuthenticateUserCommandHandler(
            IUserAuthorizationTokenRepository userAuthorizationTokenRepository,
            IUserRepository userRepository,
            IAsyncValidator<AuthenticateUserCommand> validator,
            IUnitOfWork unitOfWork,
            ITokenCreator tokenCreator)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userRepository = userRepository;
            _userAuthorizationTokenValidator = validator;
            _unitOfWork = unitOfWork;
            _tokenCreator = tokenCreator;
        }

        public async Task<AuthorizationCommandResult<AuthenticateUserCommandDto>> HandleAsync(AuthenticateUserCommand command)
        {
            ValidationResult validationResult = await _userAuthorizationTokenValidator.ValidationAsync(command);

            if (validationResult.IsFail)
            {
                return new AuthorizationCommandResult<AuthenticateUserCommandDto>(validationResult);
            }

            User user = await _userRepository.GetByLoginAsync(command.Login);
            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetTokenByUserIdAsync(user.Id);
            if (token is not null)
            {
                await _userAuthorizationTokenRepository.DeleteAsync(token);
            }

            string accessToken = _tokenCreator.CreateAccessToken(token.UserId);
            string refreshToken = _tokenCreator.CreateRefreshToken();

            UserAuthorizationToken newToken = new UserAuthorizationToken(
                user.Id,
                refreshToken,
                command.RefreshTokenExpiryDate);
            _userAuthorizationTokenRepository.Add(newToken);

            await _unitOfWork.CommitAsync();

            AuthenticateUserCommandDto authenticateUserCommandDto = new AuthenticateUserCommandDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return new AuthorizationCommandResult<AuthenticateUserCommandDto>(authenticateUserCommandDto);
        }
    }
}
