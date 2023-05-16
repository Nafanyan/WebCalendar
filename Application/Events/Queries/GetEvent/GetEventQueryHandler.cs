using Domain.Entities;
using Domain.Repositories;
using Application.Result;
using Application.Interfaces;

namespace Application.Events.Queries.GetEvent
{

    public class GetEventQueryHandler : IQueryHandler<Event, GetEventQuery>
    {
        private readonly IEventRepository _eventRepository;
        private GetEventQueryValidation _eventQueryValidation;

        public GetEventQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _eventQueryValidation = new GetEventQueryValidation(eventRepository);
        }

        public async Task<ResultQuery<Event>> Handle(GetEventQuery getEventQuery)
        {
            string msg = _eventQueryValidation.Validation(getEventQuery);
            if (msg == "Ok")
            {
                EventPeriod eventPeriod = new EventPeriod(getEventQuery.StartEvent, getEventQuery.EndEvent);
                return new ResultQuery<Event>(await _eventRepository.GetEvent(getEventQuery.UserId, eventPeriod), msg);
            }
            return new ResultQuery<Event>(msg);
        }
    }
}
