using Application.Result;

namespace Application.Users.Commands
{
    public interface IUserCommandHandler<T> where T : class
    {
        Task<ResultCommand> Handle(T command);
    }
}
