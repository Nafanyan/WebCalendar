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

        public async Task<QueryResult<Event>> HandleAsync(GetEventQuery getEventQuery)
        {
            ValidationResult validationResult = await _eventQueryValidator.ValidationAsync(getEventQuery);
            if (!validationResult.IsFail)
            {
                Event foundEvent = await _eventRepository.GetEventAsync(getEventQuery.UserId,
                    getEventQuery.StartEvent, getEventQuery.EndEvent);
                return new QueryResult<Event>( foundEvent);
            }
            return new QueryResult<Event>(validationResult);
        }
    }
}
