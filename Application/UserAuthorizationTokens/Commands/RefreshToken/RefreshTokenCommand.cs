namespace Application.UserAuthorizationTokens.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; init; }
        public DateTime RefreshTokenExpiryDate { get; init; }
    }
}
