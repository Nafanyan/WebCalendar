namespace Domain.Entitys
{
    public class User
    {
        public long Id { get; init; }
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }

        public User(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }

        public void SetLogin(string login)
        {
            Login = login;
        }
        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
