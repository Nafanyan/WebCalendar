using WebCalendar.Domain.Events;

namespace WebCalendar.Domain.Users
{
    public class User
    {
        public long Id { get; init; }
        public List<Event> Events { get; private set; }
        public string Login { get; private set; }
        private string PasswordHash { get; set; }

        public User(string login, string passwordHash)
        {
            Events = new List<Event>();
            Login = login;
            PasswordHash = passwordHash;
        }

        public void UpdateLogin(string login)
        {
            Login = login;
        }
        public bool UpdatePasswordHash(string oldPasswordHash, string newPasswordHash)
        {
            if (CheckPasswordHash(oldPasswordHash))
            {
                PasswordHash = newPasswordHash;
                return true;
            }

            return false;
        }
        public bool CheckPasswordHash(string passwordHash) => PasswordHash == passwordHash;


    }
}
