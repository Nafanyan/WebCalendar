using WebCalendar.Application.Events.Commands;
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
            _validationEvent.CheckingContentInRepository(updateEventCommand.Id);
            _validationEvent.CheckingTheRecord(updateEventCommand.Record);
            _validationEvent.CheckingDateForNull(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            _validationEvent.CheckingTheDate(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
        }
    }
}
