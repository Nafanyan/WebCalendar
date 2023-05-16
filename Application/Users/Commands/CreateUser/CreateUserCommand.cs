namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
