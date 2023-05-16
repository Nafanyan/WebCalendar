using Application.Result;

namespace Application.Interfaces
{
    public interface ICommandHandler<T> where T : class
    {
        Task<ResultCommand> Handle(T command);
    }
}
