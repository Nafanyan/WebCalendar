using Domain.Entitys;
namespace Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByLogin(string login);
    }
}
