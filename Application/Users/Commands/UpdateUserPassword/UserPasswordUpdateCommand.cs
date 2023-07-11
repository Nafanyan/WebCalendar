namespace Application.Users.Commands.UpdateUserPassword
{
    public class UserPasswordUpdateCommand
    {
        public long Id { get; init; }
        public string OldPasswordHash { get; init; }
        public string NewPasswordHash { get; init; }
    }
}
