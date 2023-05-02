namespace WebCalendar.Application.Users.UsersCreating
{
    public class AddUserCommand
    {
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
