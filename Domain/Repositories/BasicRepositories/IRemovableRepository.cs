namespace Domain.Repositories.BasicRepositories
{
    public interface IRemovableRepository<TEntety> where TEntety : class
    {
        Task DeleteAsync(TEntety entety);
    }
}
