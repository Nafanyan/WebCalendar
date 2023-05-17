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
        private readonly IAsyncValidator<GetEventQuery> _eventQueryValidator;

        public GetEventQueryHandler(IEventRepository eventRepository, IAsyncValidator<GetEventQuery> validator)
        {
            _eventRepository = eventRepository;
            _eventQueryValidator = validator;
        }

        public async Task<QueryResult<Event>> Handle(GetEventQuery getEventQuery)
        {
            ValidationResult validationResult = await _eventQueryValidator.Validation(getEventQuery);
            if (!validationResult.IsFail)
            {
                EventPeriod eventPeriod = new EventPeriod(getEventQuery.StartEvent, getEventQuery.EndEvent);
                Event foundEvent = await _eventRepository.GetEvent(getEventQuery.UserId, eventPeriod);
                return new QueryResult<Event>(validationResult, foundEvent);
            }
            return new QueryResult<Event>(validationResult);
        }
    }
}
