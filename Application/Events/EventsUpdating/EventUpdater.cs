using WebCalendar.Domain.Events;

namespace WebCalendar.Application.Events.EventsUpdating
{
    public interface IEventUpdator
    {
        public void Update(UpdateEventCommand updateEventCommand);
    }
    public class EventUpdater : BaseEventUsCase, IEventUpdator
    {
        public EventUpdater(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Update(UpdateEventCommand updateEventCommand)
        {
            ValidationCheck(updateEventCommand);

            Event e = _eventRepository.GetById(updateEventCommand.Id);
            e.UpdateRecord(updateEventCommand.Record);
            e.UpdateDescription(updateEventCommand.Description);
            e.UpdateDateEvent(updateEventCommand.StartEvent, updateEventCommand.EndEvent);

            _eventRepository.Update(e);
        }

        private void ValidationCheck(UpdateEventCommand updateEventCommand)
        {
            _validationEvent.ValueNotFound(updateEventCommand.Id);
            _validationEvent.NameNull(updateEventCommand.Record);
            _validationEvent.DateNull(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            _validationEvent.DateСorrectness(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
        }
    }
}
