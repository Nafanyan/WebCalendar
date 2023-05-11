namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
