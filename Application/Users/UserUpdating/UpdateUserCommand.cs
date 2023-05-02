namespace WebCalendar.Application.Users.UserUpdating
{
    public class UpdateUserCommand
    {
        public long Id { get; init; }
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
