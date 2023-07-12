namespace Application.Users.Commands.DeleteUser
{
    public class UserDeleteCommand
    {
        public long Id { get; init; }
        public string PasswordHash { get; init; }
    }
}
