using Application.Result;

namespace Application.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<CommandResult> Handle(TCommand command);
    }
}
