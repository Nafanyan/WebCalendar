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
        private readonly CreateEventCommandValidation _addEventCommandValidation;

        public CreateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _addEventCommandValidation = new CreateEventCommandValidation();
        }

        public async Task<CommandResult> Handle(CreateEventCommand createEventCommand)
        {
            ValidationResult validationResult = _addEventCommandValidation.Validation(createEventCommand);
            if (!validationResult.IsFail)
            {
                EventPeriod eventPeriod = new EventPeriod(createEventCommand.StartEvent, createEventCommand.EndEvent);
                Event newEvent = new Event(createEventCommand.Name, createEventCommand.Description, eventPeriod);
                await _eventRepository.Add(newEvent);
            }
            return new CommandResult(validationResult);
        }
    }
}
