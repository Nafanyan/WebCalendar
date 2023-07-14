using Application.Users.DTOs;
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
            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetByUserIdAsync(user.Id);
            if (token is not null)
            {
                _userAuthorizationTokenRepository.Delete(token);
            }

            string accessToken = _tokenCreator.CreateAccessToken(user.Id);
            string refreshToken = _tokenCreator.CreateRefreshToken();

            UserAuthorizationToken newToken = new UserAuthorizationToken(
                user.Id,
                refreshToken,
                command.NewRefreshTokenExpiryDate);
            _userAuthorizationTokenRepository.Add(newToken);

            await _unitOfWork.CommitAsync();

            AuthenticateUserCommandDto authenticateUserCommandResult = new AuthenticateUserCommandDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return new AuthorizationCommandResult<AuthenticateUserCommandDto>(authenticateUserCommandResult);
        }
    }
}
