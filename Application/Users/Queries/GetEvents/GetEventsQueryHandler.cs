using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public interface IGetEventsQueryHandler
    {
        IReadOnlyList<Event> Execute(GetEventsQuery getEventsQuery);
    }

    public class GetEventsQueryHandler : BaseUserUseCase, IGetEventsQueryHandler
    {
        public GetEventsQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public IReadOnlyList<Event> Execute(GetEventsQuery getEventsQuery)
        {
            QueryValidation(getEventsQuery);
            return _userRepository.GetEvents(getEventsQuery.UserId).Result;
        }
        private void QueryValidation(GetEventsQuery getEventsQuery)
        {
            _validationUser.ValueNotFound(getEventsQuery.UserId);
        }
    }
}
