using Domain.Entities;
using Domain.Repositories;

namespace WebCalendar.Application.Events.EventsCreating
{
    public interface IEventCreator
    {
        void Create(AddEventCommand addEventCommand);
    }

    public class EventCreator : BaseEventUseCase, IEventCreator
    {
        public EventCreator(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Create(AddEventCommand addEventCommand)
        {
            ValidationCheck(addEventCommand);

            EventPeriod eventPeriod = new EventPeriod(addEventCommand.StartEvent, addEventCommand.EndEvent);
            Event e = new Event(addEventCommand.Name, addEventCommand.Description, eventPeriod);
            _eventRepository.Add(e);
        }
        private void ValidationCheck(AddEventCommand addEventCommand)
        {
            _validationEvent.NameNull(addEventCommand.Name);
            _validationEvent.DateNull(addEventCommand.StartEvent, addEventCommand.EndEvent);
            _validationEvent.DateСorrectness(addEventCommand.StartEvent, addEventCommand.EndEvent);
        }
    }
}
