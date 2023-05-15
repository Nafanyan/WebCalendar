using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : BaseEventHandler, IEventCommandHandler<DeleteEventCommand>
    {
        private readonly DeleteEventCommandValidation _deleteEventCommandValidation;

        public DeleteEventCommandHandler(IEventRepository eventRepository) : base(eventRepository)
        {
            _deleteEventCommandValidation = new DeleteEventCommandValidation(eventRepository);
        }

        public async Task<ResultCommand> Handle(DeleteEventCommand deleteEventCommand)
        {
            string msg = _deleteEventCommandValidation.Validation(deleteEventCommand);
            if (msg == "Ok")
            {
                EventPeriod eventPeriod = new EventPeriod(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
                Event foundEvent = await EventRepository.GetEvent(deleteEventCommand.UserId, eventPeriod);
                await EventRepository.Delete(foundEvent);
            }
            return new ResultCommand(msg);
        }
    }
}
