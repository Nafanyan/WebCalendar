using Application.Result;

namespace Application.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<AuthorizationCommandResult> HandleAsync(TCommand command);
    }
}
