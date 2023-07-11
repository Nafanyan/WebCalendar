namespace Application.UserAuthorizationTokens.DTOs
{
    public class GetTokenQueryDto
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }
}
