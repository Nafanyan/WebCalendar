using Domain.Entitys;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        IReadOnlyList<User> GetAll();
        User GetById(long id);
        User GetByLogin(string login);
        long Add(User user);
        void Update(User user);
        void Delete(long id);
    }
}
