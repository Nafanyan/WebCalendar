namespace Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
