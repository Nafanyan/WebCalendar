namespace Application.UserAuthorizationTokens.Commands.UserAuthorization
{
    public class UserAuthorizationCommand
    {
        public long UserId { get; init; }
        public string Login { get; init; }
        public string PasswordHash { get; init; }
        public string RefreshToken { get; init; }
    }
}
