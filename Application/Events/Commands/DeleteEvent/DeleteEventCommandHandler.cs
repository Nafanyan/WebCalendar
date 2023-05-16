using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly DeleteEventCommandValidation _deleteEventCommandValidation;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _deleteEventCommandValidation = new DeleteEventCommandValidation(eventRepository);
        }

        public async Task<CommandResult> Handle(DeleteEventCommand deleteEventCommand)
        {
            ValidationResult validationResult = _deleteEventCommandValidation.Validation(deleteEventCommand);
            if (!validationResult.IsFail)
            {
                EventPeriod eventPeriod = new EventPeriod(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
                Event foundEvent = await _eventRepository.GetEvent(deleteEventCommand.UserId, eventPeriod);
                await _eventRepository.Delete(foundEvent);
            }
            return new CommandResult(validationResult);
        }
    }
}
