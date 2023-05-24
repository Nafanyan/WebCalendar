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

        public async Task<CommandResult> HandleAsync(DeleteEventCommand deleteEventCommand)
        {
            ValidationResult validationResult = await _deleteEventCommandValidator.ValidationAsync(deleteEventCommand);
            if (!validationResult.IsFail)
            {
                Event foundEvent = await _eventRepository.GetEventAsync(deleteEventCommand.UserId,
                    deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
                await _eventRepository.DeleteAsync(foundEvent);
            }
            return new CommandResult(validationResult);
        }
    }
}
