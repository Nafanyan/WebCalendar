namespace Domain.Repositories.BasicRepositories
{
    public interface IAddedRepository<TEntety> where TEntety : class
    {
        Task AddAsync(TEntety entety);
    }
}
