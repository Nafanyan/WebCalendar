namespace Application.UserAuthorizationTokens.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand
    {
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }   
}
