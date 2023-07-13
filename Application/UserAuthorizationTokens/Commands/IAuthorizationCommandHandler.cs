using Application.Result;

namespace Application.UserAuthorizationTokens.Commands
{
    public interface IAuthorizationCommandHandler<TResult, TCommand>
        where TResult : class
        where TCommand : class
    {
        Task<AuthorizationCommandResult<TResult>> HandleAsync(TCommand command);
    }
}
