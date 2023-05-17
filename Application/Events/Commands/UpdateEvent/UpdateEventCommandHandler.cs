using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : ICommandHandler<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncValidator<UpdateEventCommand> _updateEventCommandValidator;

        public UpdateEventCommandHandler(IEventRepository eventRepository, IAsyncValidator<UpdateEventCommand> validator) 
        {
            _eventRepository = eventRepository;
            _updateEventCommandValidator = validator;
        }

        public async Task<CommandResult> Handle(UpdateEventCommand updateEventCommand)
        {
            ValidationResult validationResult = await _updateEventCommandValidator.Validation(updateEventCommand);
            if (!validationResult.IsFail)
            {
                EventPeriod eventPeriod = new EventPeriod(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
                Event oldEvent = await _eventRepository.GetEvent(updateEventCommand.UserId, eventPeriod);
                oldEvent.SetName(updateEventCommand.Name);
                oldEvent.SetDescription(updateEventCommand.Description);
                oldEvent.SetDateEvent(eventPeriod);
            }
            return new CommandResult(validationResult);
        }
    }
}
