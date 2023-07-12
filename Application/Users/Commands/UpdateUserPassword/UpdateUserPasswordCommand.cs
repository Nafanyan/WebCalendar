namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommand
    {
        public long Id { get; init; }
        public string OldPasswordHash { get; init; }
        public string NewPasswordHash { get; init; }
    }
}
