using Application.Tokens.CreateToken;
using Application.Tokens;
using Application.Users.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UserAuthorizationTokens.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommandDto, AuthenticateUserCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<AuthenticateUserCommand> _userAuthorizationTokenValidator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenConfiguration _tokenConfiguration;
        private readonly TokenCreator _tokenCreator;

        public AuthenticateUserCommandHandler(
            IUserAuthorizationTokenRepository userAuthorizationTokenRepository,
            IUserRepository userRepository,
            IAsyncValidator<AuthenticateUserCommand> validator,
            IUnitOfWork unitOfWork,
            ITokenConfiguration tokenConfiguration)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userRepository = userRepository;
            _userAuthorizationTokenValidator = validator;
            _unitOfWork = unitOfWork;
            _tokenConfiguration = tokenConfiguration;
            _tokenCreator = new TokenCreator(tokenConfiguration);
        }

        public async Task<CommandResult<AuthenticateUserCommandDto>> HandleAsync(AuthenticateUserCommand command)
        {
            ValidationResult validationResult = await _userAuthorizationTokenValidator.ValidationAsync(command);

            if (validationResult.IsFail)
            {
                return new CommandResult<AuthenticateUserCommandDto>(validationResult);
            }

            User user = await _userRepository.GetByLoginAsync(command.Login);
            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetByUserIdAsync(user.Id);
            if (token is not null)
            {
                _userAuthorizationTokenRepository.Delete(token);
            }

            string accessToken = _tokenCreator.CreateAccessToken(user.Id);
            string refreshToken = _tokenCreator.CreateRefreshToken();

            DateTime refreshTokenExpiryDate = DateTime.Now.AddDays(int.Parse(_tokenConfiguration.GetRefreshTokenValidityInDays()));

            UserAuthorizationToken newToken = new UserAuthorizationToken(
                user.Id,
                refreshToken,
                refreshTokenExpiryDate);
            _userAuthorizationTokenRepository.Add(newToken);

            await _unitOfWork.CommitAsync();

            AuthenticateUserCommandDto authenticateUserCommandResult = new AuthenticateUserCommandDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return new CommandResult<AuthenticateUserCommandDto>(authenticateUserCommandResult);
        }
    }
}
