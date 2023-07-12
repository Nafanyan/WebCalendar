namespace Application.UserAuthorizationTokens.Commands.VerificationToken
{
    public class VerificationTokenCommand
    {
        public long UserId { get; init; }
        public string RefreshToken { get; init; }
    }
}
