using Application.Result;

namespace Application.CQRSInterfaces
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<CommandResult> HandleAsync(TCommand command);
    }
}
