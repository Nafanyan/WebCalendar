
namespace Application.UserAuthorizationTokens.Commands
{
    public interface ICommandHandler<TResult, TCommand>
        where TResult : class
        where TCommand : class
    {
        Task<CommandResult<TResult>> HandleAsync(TCommand command);
    }
}
