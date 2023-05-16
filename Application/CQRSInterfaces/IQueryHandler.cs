using Application.Result;

namespace Application.Interfaces
{
    public interface IQueryHandler<T, Q>
        where T : class
        where Q : class
    {
        Task<QueryResult<T>> Handle(Q query);
    }
}