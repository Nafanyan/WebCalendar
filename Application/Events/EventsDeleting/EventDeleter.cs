using Application.Events.EventsDeleting;
using Domain.Entities;
using Domain.Repositories;

namespace WebCalendar.Application.Events.EventsDeleting
{
    public interface IEventDeletor
    {
        public void Delete(DeleteEventCommand deleteEventCommand);
    }

    public class EventDeleter : BaseEventUseCase, IEventDeletor
    {
        private EventPeriod _eventPeriod;
        public EventDeleter(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Delete(DeleteEventCommand deleteEventCommand)
        {
            ValidationCheck(deleteEventCommand);

            Event e = _eventRepository.GetEvent(deleteEventCommand.UserId, _eventPeriod).Result;
            _eventRepository.Delete(e);
        }
        private void ValidationCheck(DeleteEventCommand deleteEventCommand)
        {
            _validationEvent.DateNull(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
            _validationEvent.DateСorrectness(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
            _eventPeriod = new EventPeriod(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);

            _validationEvent.ValueNotFound(deleteEventCommand.UserId, _eventPeriod);
        }
    }
}
