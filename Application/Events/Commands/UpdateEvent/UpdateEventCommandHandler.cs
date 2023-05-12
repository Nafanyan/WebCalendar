using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.UpdateEvent
{
    public interface IUpdateEventCommandHandler
    {
        public void Execute(UpdateEventCommand updateEventCommand);
    }
    public class UpdateEventCommandHandler : BaseEventUseCase, IUpdateEventCommandHandler
    {
        private EventPeriod _eventPeriod;

        public UpdateEventCommandHandler(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Execute(UpdateEventCommand updateEventCommand)
        {
            CommandValidation(updateEventCommand);

            Event e = _eventRepository.GetEvent(updateEventCommand.UserId, _eventPeriod).Result;
            e.SetName(updateEventCommand.Name);
            e.SetDescription(updateEventCommand.Description);
            e.SetDateEvent(_eventPeriod);
        }
        private void CommandValidation(UpdateEventCommand updateEventCommand)
        {
            _validationEvent.DateNull(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            _validationEvent.DateСorrectness(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            _eventPeriod = new EventPeriod(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            _validationEvent.ValueNotFound(updateEventCommand.UserId, _eventPeriod);
        }
    }
}
