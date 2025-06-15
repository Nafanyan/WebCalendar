using Application.Result;

namespace Application.Interfaces
{
    public interface IQueryHandler<TResult, TQuery>
        where TResult : class
        where TQuery : class
    {
        Task<QueryResult<TResult>> HandleAsync( TQuery query );
    }
}