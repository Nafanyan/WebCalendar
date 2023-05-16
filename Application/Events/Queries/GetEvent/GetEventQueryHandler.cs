using Domain.Entities;
using Domain.Repositories;
using Application.Result;
using Application.Interfaces;
using Application.Validation;

namespace Application.Events.Queries.GetEvent
{

    public class GetEventQueryHandler : IQueryHandler<Event, GetEventQuery>
    {
        private readonly IEventRepository _eventRepository;
        private IValidator<GetEventQuery> _eventQueryValidation;

        public GetEventQueryHandler(IEventRepository eventRepository, IValidator<GetEventQuery> validator)
        {
            _eventRepository = eventRepository;
            _eventQueryValidation = validator;
        }

        public async Task<QueryResult<Event>> Handle(GetEventQuery getEventQuery)
        {
            ValidationResult validationResult = _eventQueryValidation.Validation(getEventQuery);
            if (!validationResult.IsFail)
            {
                EventPeriod eventPeriod = new EventPeriod(getEventQuery.StartEvent, getEventQuery.EndEvent);
                return new QueryResult<Event>(validationResult, await _eventRepository.GetEvent(getEventQuery.UserId, eventPeriod));
            }
            return new QueryResult<Event>(validationResult);
        }
    }
}
