using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Queries.GetEvent
{
    public interface IGetEventQueryHandler
    {
        public Event Execute(GetEventQuery getEventQuery);
    }
    public class GetEventQueryHandler : BaseEventUseCase, IGetEventQueryHandler
    {
        private EventPeriod _eventPeriod;

        public GetEventQueryHandler(IEventRepository eventRepository) : base(eventRepository)
        {
        }
        
        public Event Execute(GetEventQuery getEventQuery)
        {
            QueryValidation(getEventQuery);
            return _eventRepository.GetEvent(getEventQuery.UserId, _eventPeriod).Result;
        }
        private void QueryValidation(GetEventQuery getEventQuery)
        {
            _validationEvent.DateNull(getEventQuery.StartEvent, getEventQuery.EndEvent);
            _validationEvent.DateСorrectness(getEventQuery.StartEvent, getEventQuery.EndEvent);
            _eventPeriod = new EventPeriod(getEventQuery.StartEvent, getEventQuery.EndEvent);
            _validationEvent.ValueNotFound(getEventQuery.UserId, _eventPeriod);
        }
    }
}
