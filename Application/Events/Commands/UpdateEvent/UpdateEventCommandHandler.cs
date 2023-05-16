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
        private readonly UpdateEventCommandValidation _updateEventCommandValidation;

        public UpdateEventCommandHandler(IEventRepository eventRepository) 
        {
            _eventRepository = eventRepository;
            _updateEventCommandValidation = new UpdateEventCommandValidation(eventRepository);
        }

        public async Task<CommandResult> Handle(UpdateEventCommand updateEventCommand)
        {
            ValidationResult validationResult = _updateEventCommandValidation.Validation(updateEventCommand);
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
