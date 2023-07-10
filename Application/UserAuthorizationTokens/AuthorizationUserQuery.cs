namespace Application.UserAuthorizationTokens
{
    public class AuthorizationUserQuery
    {
        public long UserId { get; init; }
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
