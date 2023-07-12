namespace Application.UserAuthorizationTokens.DTOs
{
    public class AuthenticateUserCommandDto
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }
}
