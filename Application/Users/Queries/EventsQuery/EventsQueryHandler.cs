using Application.Interfaces;
using Application.Result;
using Application.Users.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.EventsQuery
{
    public class EventsQueryHandler : IQueryHandler<IReadOnlyList<EventsQueryDto>, EventsQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<EventsQuery> _getEventsQueryValidator;

        public EventsQueryHandler(IUserRepository userRepository, IAsyncValidator<EventsQuery> validator)
        {
            _userRepository = userRepository;
            _getEventsQueryValidator = validator;
        }

        public async Task<QueryResult<IReadOnlyList<EventsQueryDto>>> HandleAsync(EventsQuery getEventsQuery)
        {
            ValidationResult validationResult = await _getEventsQueryValidator.ValidationAsync(getEventsQuery);
            if (!validationResult.IsFail)
            {
                IReadOnlyList<Event> events = await _userRepository.GetEventsAsync(getEventsQuery.UserId,
                    getEventsQuery.StartEvent, getEventsQuery.EndEvent);

                List<EventsQueryDto> getEventsQueryDtos = events.Select(e => new EventsQueryDto
                {
                    UserId = e.UserId,
                    Name = e.Name,
                    Description = e.Description,
                    StartEvent = e.StartEvent,
                    EndEvent = e.EndEvent
                }).ToList();
                return new QueryResult<IReadOnlyList<EventsQueryDto>>(getEventsQueryDtos);
            }
            return new QueryResult<IReadOnlyList<EventsQueryDto>>(validationResult);
        }
    }
}
