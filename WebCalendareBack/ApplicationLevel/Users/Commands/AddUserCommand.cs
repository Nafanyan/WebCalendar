namespace WebCalendar.Application.Users.Commands
{
    public class AddUserCommand
    {
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
