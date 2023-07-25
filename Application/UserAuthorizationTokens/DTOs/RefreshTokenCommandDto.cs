namespace Application.UserAuthorizationTokens.DTOs
{
    public class RefreshTokenCommandDto
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }
}
