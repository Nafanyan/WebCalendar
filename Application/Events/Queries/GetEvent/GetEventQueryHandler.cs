using Domain.Entities;
using Domain.Repositories;
using Application.Result;

namespace Application.Events.Queries.GetEvent
{

    public class GetEventQueryHandler : BaseEventHandler, IEventQueryHandler<Event, GetEventQuery>
    {
        private GetEventQueryValidation _eventQueryValidation;

        public GetEventQueryHandler(IEventRepository eventRepository) : base(eventRepository)
        {
            _eventQueryValidation = new GetEventQueryValidation(eventRepository);
        }

        public async Task<ResultQuery<Event>> Handle(GetEventQuery getEventQuery)
        {
            string msg = _eventQueryValidation.Validation(getEventQuery);
            if (msg == "Ok")
            {
                EventPeriod eventPeriod = new EventPeriod(getEventQuery.StartEvent, getEventQuery.EndEvent);
                return new ResultQuery<Event>(await EventRepository.GetEvent(getEventQuery.UserId, eventPeriod), msg);
            }
            return new ResultQuery<Event>(msg);
        }
    }
}
