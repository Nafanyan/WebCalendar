using Application.Tokens;
using Application.Tokens.CreateToken;
using Application.UserAuthorizationTokens.DTOs;
using Application.Validation;
using Application.Entities;
using Application.Repositories;
using Application.CQRSInterfaces;
using Application.Result;

namespace Application.UserAuthorizationTokens.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommandDto, RefreshTokenCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IAsyncValidator<RefreshTokenCommand> _userAuthorizationTokenValidator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenConfiguration _tokenConfiguration;
        private readonly TokenCreator _tokenCreator;

        public RefreshTokenCommandHandler(
            IUserAuthorizationTokenRepository userAuthorizationTokenRepository,
            IAsyncValidator<RefreshTokenCommand> validator,
            IUnitOfWork unitOfWork,
            ITokenConfiguration tokenConfiguration )
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userAuthorizationTokenValidator = validator;
            _unitOfWork = unitOfWork;
            _tokenConfiguration = tokenConfiguration;
            _tokenCreator = new TokenCreator( _tokenConfiguration );

        }

        public async Task<CommandResult<RefreshTokenCommandDto>> HandleAsync( RefreshTokenCommand command )
        {
            ValidationResult validationResult = await _userAuthorizationTokenValidator.ValidationAsync( command );

            if( validationResult.IsFail )
            {
                return new CommandResult<RefreshTokenCommandDto>( validationResult );
            }

            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetByRefreshTokenAsync( command.RefreshToken );
            _userAuthorizationTokenRepository.Delete( token );

            string accessToken = _tokenCreator.CreateAccessToken( token.UserId );
            string refreshToken = _tokenCreator.CreateRefreshToken();
            DateTime refreshTokenExpiryDate = DateTime.Now.AddDays( _tokenConfiguration.GetRefreshTokenValidityInDays() );

            UserAuthorizationToken newToken = new UserAuthorizationToken(
                token.UserId,
                refreshToken,
                refreshTokenExpiryDate );
            _userAuthorizationTokenRepository.Add( newToken );

            await _unitOfWork.CommitAsync();

            RefreshTokenCommandDto refreshTokenCommandResult = new RefreshTokenCommandDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return new CommandResult<RefreshTokenCommandDto>( refreshTokenCommandResult );
        }
    }
}
