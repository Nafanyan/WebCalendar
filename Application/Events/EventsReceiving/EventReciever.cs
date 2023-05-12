using Application.Events.EventsDeleting;
using Application.Events.EventsReceiving;
using Domain.Entities;
using Domain.Repositories;

namespace WebCalendar.Application.Events.EventsReciever
{
    public interface IEventReciever
    {
        public Event GetEvent(ReceiveEventCommand receiveEventCommand);
    }
    public class EventReciever : BaseEventUseCase, IEventReciever
    {
        private EventPeriod _eventPeriod;

        public EventReciever(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public Event GetEvent(ReceiveEventCommand receiveEventCommand)
        {
            ValidationCheck(receiveEventCommand);
            return _eventRepository.GetEvent(receiveEventCommand.UserId, _eventPeriod).Result;
        }
        private void ValidationCheck(ReceiveEventCommand receiveEventCommand)
        {
            _validationEvent.DateNull(receiveEventCommand.StartEvent, receiveEventCommand.EndEvent);
            _validationEvent.DateСorrectness(receiveEventCommand.StartEvent, receiveEventCommand.EndEvent);
            _eventPeriod = new EventPeriod(receiveEventCommand.StartEvent, receiveEventCommand.EndEvent);

            _validationEvent.ValueNotFound(receiveEventCommand.UserId, _eventPeriod);
        }
    }
}
