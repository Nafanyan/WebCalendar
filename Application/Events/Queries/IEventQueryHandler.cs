using Application.Result;

namespace Application.Events.Queries
{
    public interface IEventQueryHandler<T, Q> 
        where T : class 
        where Q : class
    {
        Task<ResultQuery<T>> Handle(Q query);
    }
}
