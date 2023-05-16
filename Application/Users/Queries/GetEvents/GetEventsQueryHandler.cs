using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryHandler : IQueryHandler<IReadOnlyList<Event>, GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<GetEventsQuery> _getEventsQueryValidation;

        public GetEventsQueryHandler(IUserRepository userRepository, IValidator<GetEventsQuery> validator)
        {
            _userRepository = userRepository;
            _getEventsQueryValidation = validator;
        }

        public async Task<QueryResult<IReadOnlyList<Event>>> Handle(GetEventsQuery getEventsQuery)
        {
            ValidationResult validationResult = _getEventsQueryValidation.Validation(getEventsQuery);
            if (!validationResult.IsFail)
            {
                return new QueryResult<IReadOnlyList<Event>>(validationResult, await _userRepository.GetEvents(getEventsQuery.UserId));
            }
            return new QueryResult<IReadOnlyList<Event>>(validationResult);
        }
    }
}
