namespace Application.UserAuthorizationTokens.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public long UserId { get; init; }
        public string Login { get; init; }
        public string PasswordHash { get; init; }
        public int TokenValidityDays { get; init; }
        public string RefreshToken { get; init; }
    }
}
