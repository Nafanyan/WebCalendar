namespace Application.UserAuthorizationTokens.Commands.TokenVerification
{
    public class TokenVerificationCommand
    {
        public long UserId { get; init; }
        public string RefreshToken { get; init; }
    }
}
