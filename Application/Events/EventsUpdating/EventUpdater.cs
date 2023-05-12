using Application.Events.EventsReceiving;
using Domain.Entities;
using Domain.Repositories;

namespace WebCalendar.Application.Events.EventsUpdating
{
    public interface IEventUpdator
    {
        public void Update(UpdateEventCommand updateEventCommand);
    }
    public class EventUpdater : BaseEventUseCase, IEventUpdator
    {
        private EventPeriod _eventPeriod;

        public EventUpdater(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Update(UpdateEventCommand updateEventCommand)
        {
            ValidationCheck(updateEventCommand);

            Event e = _eventRepository.GetEvent(updateEventCommand.UserId, _eventPeriod).Result;
            e.SetName(updateEventCommand.Name);
            e.SetDescription(updateEventCommand.Description);
            e.SetDateEvent(_eventPeriod);
        }
        private void ValidationCheck(UpdateEventCommand updateEventCommand)
        {
            _validationEvent.DateNull(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            _validationEvent.DateСorrectness(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            _eventPeriod = new EventPeriod(updateEventCommand.StartEvent, updateEventCommand.EndEvent);

            _validationEvent.ValueNotFound(updateEventCommand.UserId, _eventPeriod);
        }
}
