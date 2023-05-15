using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IEventCommandHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly DeleteEventCommandValidation _deleteEventCommandValidation;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _deleteEventCommandValidation = new DeleteEventCommandValidation(eventRepository);
        }

        public async Task<ResultCommand> Handle(DeleteEventCommand deleteEventCommand)
        {
            string msg = _deleteEventCommandValidation.Validation(deleteEventCommand);
            if (msg == "Ok")
            {
                EventPeriod eventPeriod = new EventPeriod(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
                Event foundEvent = await _eventRepository.GetEvent(deleteEventCommand.UserId, eventPeriod);
                await _eventRepository.Delete(foundEvent);
            }
            return new ResultCommand(msg);
        }
    }
}
