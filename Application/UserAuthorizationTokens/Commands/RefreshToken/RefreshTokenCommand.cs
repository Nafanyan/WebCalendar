namespace Application.UserAuthorizationTokens.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; init; }
        public DateTime NewRefreshTokenExpiryDate { get; init; }
    }
}
