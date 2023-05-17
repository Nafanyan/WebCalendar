using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : ICommandHandler<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncValidator<UpdateEventCommand> _updateEventCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventCommandHandler(IEventRepository eventRepository, 
            IAsyncValidator<UpdateEventCommand> validator,
            IUnitOfWork unitOfWork) 
        {
            _eventRepository = eventRepository;
            _updateEventCommandValidator = validator;
            _unitOfWork = unitOfWork;
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
                await _unitOfWork.Commit();
            }
            return new CommandResult(validationResult);
        }
    }
}
