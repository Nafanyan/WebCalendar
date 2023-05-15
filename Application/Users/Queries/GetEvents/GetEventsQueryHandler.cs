using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryHandler : IUserQueryHandler<IReadOnlyList<Event>, GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly GetEventsQueryValidation _getEventsQueryValidation;

        public GetEventsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _getEventsQueryValidation = new GetEventsQueryValidation(userRepository);
        }

        public async Task<ResultQuery<IReadOnlyList<Event>>> Handle(GetEventsQuery getEventsQuery)
        {
            string msg = _getEventsQueryValidation.Validation(getEventsQuery);
            if (msg == "Ok")
            {
                return new ResultQuery<IReadOnlyList<Event>>(await _userRepository.GetEvents(getEventsQuery.UserId), msg);
            }
            return new ResultQuery<IReadOnlyList<Event>>(msg);
        }
    }
}
