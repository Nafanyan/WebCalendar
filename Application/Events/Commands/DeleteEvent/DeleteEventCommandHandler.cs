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
        private readonly IAsyncValidator<DeleteEventCommand> _deleteEventCommandValidator;

        public DeleteEventCommandHandler(IEventRepository eventRepository, IAsyncValidator<DeleteEventCommand> validator)
        {
            _eventRepository = eventRepository;
            _deleteEventCommandValidator = validator;
        }

        public async Task<CommandResult> Handle(DeleteEventCommand deleteEventCommand)
        {
            ValidationResult validationResult = await _deleteEventCommandValidator.Validation(deleteEventCommand);
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
