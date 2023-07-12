using Application.UserAuthorizationTokens.Commands.AuthenticateUser;
using Presentation.Intranet.Api.Dtos.AuthenticationDtos;
using System.Runtime.CompilerServices;

namespace Presentation.Intranet.Api.Mappers.AuthenticationMappers
{
    public static class AuthenticateUserCommandMappers
    {
        public static AuthenticateUserCommand Map(this AuthenticationWithLoginDto authenticationWithLoginRequest, DateTime refreshTokenExpiryDate)
        {
            return new AuthenticateUserCommand
            {
                Login = authenticationWithLoginRequest.Login,
                PasswordHash = authenticationWithLoginRequest.PasswordHash,
                RefreshTokenExpiryDate = refreshTokenExpiryDate,
            };
        }
    }
}
