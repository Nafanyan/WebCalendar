namespace Domain.Repositories.BasicRepositories
{
    public interface IAddedRepository<TEntety> where TEntety : class
    {
        Task Add(TEntety entety);
    }
}
