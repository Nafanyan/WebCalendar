namespace Domain.Repositories
{
    public interface IBaseRepository<TEntety> where TEntety : class
    {
        IReadOnlyList<TEntety> GetAll();
        TEntety GetById(long id);
        void Add(TEntety entety);
        void Update(TEntety entety);
        void Delete(long id);
    }
}
