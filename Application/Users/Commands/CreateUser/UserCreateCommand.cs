namespace Application.Users.Commands.CreateUser
{
    public class UserCreateCommand
    {
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
