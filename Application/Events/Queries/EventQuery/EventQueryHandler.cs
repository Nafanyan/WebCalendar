using Domain.Entities;
using Domain.Repositories;
using Application.Result;
using Application.Interfaces;
using Application.Validation;
using Application.Events.DTOs;

namespace Application.Events.Queries.EventQuery
{
    public class EventQueryHandler : IQueryHandler<GetEventQueryDto, EventQuery>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncValidator<EventQuery> _eventQueryValidator;

        public EventQueryHandler(IEventRepository eventRepository, IAsyncValidator<EventQuery> validator)
        {
            _eventRepository = eventRepository;
            _eventQueryValidator = validator;
        }

        public async Task<QueryResult<GetEventQueryDto>> HandleAsync(EventQuery getEventQuery)
        {
            ValidationResult validationResult = await _eventQueryValidator.ValidationAsync(getEventQuery);
            if (!validationResult.IsFail)
            {
                Event foundEvent = await _eventRepository.GetEventAsync(getEventQuery.UserId,
                    getEventQuery.StartEvent, getEventQuery.EndEvent);
                GetEventQueryDto getEventQueryDto = new GetEventQueryDto
                {
                    UserId = foundEvent.UserId,
                    Name = foundEvent.Name,
                    Description = foundEvent.Description,
                    StartEvent = foundEvent.StartEvent,
                    EndEvent = foundEvent.EndEvent
                };
                return new QueryResult<GetEventQueryDto>(getEventQueryDto);
            }
            return new QueryResult<GetEventQueryDto>(validationResult);
        }
    }
}
