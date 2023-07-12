using Domain.Entities;
using Domain.Repositories;
using Application.Result;
using Application.Interfaces;
using Application.Validation;
using Application.Events.DTOs;

namespace Application.Events.Queries.EventQuery
{
    public class EventQueryHandler : IQueryHandler<GetEventQueryDto, GetEventQuery>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncValidator<GetEventQuery> _eventQueryValidator;

        public EventQueryHandler(IEventRepository eventRepository, IAsyncValidator<GetEventQuery> validator)
        {
            _eventRepository = eventRepository;
            _eventQueryValidator = validator;
        }

        public async Task<QueryResult<GetEventQueryDto>> HandleAsync(GetEventQuery getEventQuery)
        {
            ValidationResult validationResult = await _eventQueryValidator.ValidationAsync(getEventQuery);
            if (validationResult.IsFail)
            {
                return new QueryResult<GetEventQueryDto>(validationResult);
            }

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
    }
}
