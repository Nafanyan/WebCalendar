namespace Application.UserAuthorizationTokens.Commands.UserRegistration
{
    public class RegistrationUserCommand
    {
        public long UserId { get; init; }
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
