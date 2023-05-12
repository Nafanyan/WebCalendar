using Domain.Entities;
using Domain.Repositories;

namespace WebCalendar.Application.Events.EventsCreating
{
    public interface IAddEventCommandHandler
    {
        void Execute(AddEventCommand addEventCommand);
    }

    public class AddEventCommandHandler : BaseEventUseCase, IAddEventCommandHandler
    {
        public AddEventCommandHandler(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Execute(AddEventCommand addEventCommand)
        {
            CommandValidation(addEventCommand);

            EventPeriod eventPeriod = new EventPeriod(addEventCommand.StartEvent, addEventCommand.EndEvent);
            Event e = new Event(addEventCommand.Name, addEventCommand.Description, eventPeriod);
            _eventRepository.Add(e);
        }
        private void CommandValidation(AddEventCommand addEventCommand)
        {
            _validationEvent.NameNull(addEventCommand.Name);
            _validationEvent.DateNull(addEventCommand.StartEvent, addEventCommand.EndEvent);
            _validationEvent.DateСorrectness(addEventCommand.StartEvent, addEventCommand.EndEvent);
        }
    }
}
