using Application.Result;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Application.Validation;

namespace Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncValidator<CreateEventCommand> _createEventCommandValidator;

        public CreateEventCommandHandler(IEventRepository eventRepository, IAsyncValidator<CreateEventCommand> validator)
        {
            _eventRepository = eventRepository;
            _createEventCommandValidator = validator;
        }

        public async Task<CommandResult> HandleAsync(CreateEventCommand createEventCommand)
        {
            ValidationResult validationResult = await _createEventCommandValidator.ValidationAsync(createEventCommand);
            if (!validationResult.IsFail)
            {
                DateTime startEvent;
                DateTime.TryParse(createEventCommand.StartEvent, out startEvent);

                DateTime endEvent;
                DateTime.TryParse(createEventCommand.EndEvent, out endEvent);

                Event newEvent = new Event(createEventCommand.UserId, createEventCommand.Name, createEventCommand.Description,
                    startEvent, endEvent);
                await _eventRepository.AddAsync(newEvent);
            }
            return new CommandResult(validationResult);
        }
    }
}
