namespace Application.UserAuthorizationTokens.Commands.VerificationToken
{
    public class TokenVerificationCommand
    {
        public long UserId { get; init; }
        public string RefreshToken { get; init; }
    }
}
