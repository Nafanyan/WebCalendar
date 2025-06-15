using Application.Validation;
using Domain.Entities;
using Application.Repositories;

namespace Application.UserAuthorizationTokens.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : IAsyncValidator<RefreshTokenCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;

        public RefreshTokenCommandValidator( IUserAuthorizationTokenRepository userAuthorizationTokenRepository )
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
        }

        public async Task<ValidationResult> ValidationAsync( RefreshTokenCommand command )
        {
            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetByRefreshTokenAsync( command.RefreshToken );

            if( token is null )
            {
                return ValidationResult.Fail( "Требуется авторизация" );
            }

            if( DateTime.Now > token.ExpiryDate )
            {
                return ValidationResult.Fail( "Срок действия токена истек" );
            }

            return ValidationResult.Ok();
        }
    }
}
