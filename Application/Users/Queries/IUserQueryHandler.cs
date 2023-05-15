using Application.Result;

namespace Application.Users.Queries
{
    public interface IUserQueryHandler<T, Q>
        where T : class
        where Q : class
    {
        Task<ResultQuery<T>> Handle(Q query);
    }
}
