namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public long Id { get; init; }
        public string PasswordHash { get; init; }
    }
}
