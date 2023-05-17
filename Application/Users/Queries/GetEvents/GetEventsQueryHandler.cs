using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryHandler : IQueryHandler<IReadOnlyList<Event>, GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<GetEventsQuery> _getEventsQueryValidator;

        public GetEventsQueryHandler(IUserRepository userRepository, IAsyncValidator<GetEventsQuery> validator)
        {
            _userRepository = userRepository;
            _getEventsQueryValidator = validator;
        }

        public async Task<QueryResult<IReadOnlyList<Event>>> Handle(GetEventsQuery getEventsQuery)
        {
            ValidationResult validationResult = await _getEventsQueryValidator.Validation(getEventsQuery);
            if (!validationResult.IsFail)
            {
                IReadOnlyList<Event> events = await _userRepository.GetEvents(getEventsQuery.UserId);
                return new QueryResult<IReadOnlyList<Event>>(validationResult, events);
            }
            return new QueryResult<IReadOnlyList<Event>>(validationResult);
        }
    }
}
