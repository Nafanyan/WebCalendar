using Application.Result;

namespace Application.Events.Commands
{
    public interface IEventCommandHandler<T> where T : class
    {
        Task<ResultCommand> Handle(T command);
    }
}
