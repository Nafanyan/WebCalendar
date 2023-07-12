namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommand
    {
        public long Id { get; init; }
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
