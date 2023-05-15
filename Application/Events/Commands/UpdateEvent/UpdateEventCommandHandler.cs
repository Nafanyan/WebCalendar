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

        public async Task<ResultCommand> Handle(UpdateEventCommand updateEventCommand)
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
    }
}
