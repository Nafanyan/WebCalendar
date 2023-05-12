using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.DeleteEvent
{
    public interface IDeleteEventCommandHandler
    {
        public void Execute(DeleteEventCommand deleteEventCommand);
    }

    public class DeleteEventCommandHandler : BaseEventUseCase, IDeleteEventCommandHandler
    {
        private EventPeriod _eventPeriod;
        public DeleteEventCommandHandler(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Execute(DeleteEventCommand deleteEventCommand)
        {
            CommandValidation(deleteEventCommand);

            Event e = _eventRepository.GetEvent(deleteEventCommand.UserId, _eventPeriod).Result;
            _eventRepository.Delete(e);
        }
        private void CommandValidation(DeleteEventCommand deleteEventCommand)
        {
            _validationEvent.DateNull(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
            _validationEvent.DateСorrectness(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
            _eventPeriod = new EventPeriod(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
            _validationEvent.ValueNotFound(deleteEventCommand.UserId, _eventPeriod);
        }
    }
}
