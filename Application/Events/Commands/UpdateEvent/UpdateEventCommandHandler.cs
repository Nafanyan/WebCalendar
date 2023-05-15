using Application.Events.Commands.DeleteEvent;
using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : BaseEventHandler, IEventCommandHandler<UpdateEventCommand>
    {
        private readonly UpdateEventCommandValidation _updateEventCommandValidation;

        public UpdateEventCommandHandler(IEventRepository eventRepository) : base(eventRepository)
        {
            _updateEventCommandValidation = new UpdateEventCommandValidation(eventRepository);
        }

        public async Task<ResultCommand> Handler(UpdateEventCommand updateEventCommand)
        {
            string msg = _updateEventCommandValidation.Validation(updateEventCommand);
            if (msg == "Ok")
            {
                EventPeriod eventPeriod = new EventPeriod(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
                Event oldEvent = await EventRepository.GetEvent(updateEventCommand.UserId, eventPeriod);
                oldEvent.SetName(updateEventCommand.Name);
                oldEvent.SetDescription(updateEventCommand.Description);
                oldEvent.SetDateEvent(eventPeriod);
            }
            return new ResultCommand(msg);
        }
        private void CommandValidation(UpdateEventCommand updateEventCommand)
        {
            ValidationEvent.DateNull(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            ValidationEvent.DateСorrectness(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            _eventPeriod = new EventPeriod(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            ValidationEvent.ValueNotFound(updateEventCommand.UserId, _eventPeriod);
        }
    }
}
