using Application.Events.Commands.DeleteEvent;
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

        public async Task<CommandResult> HandleAsync(UpdateEventCommand updateEventCommand)
        {
            ValidationResult validationResult = await _updateEventCommandValidator.ValidationAsync(updateEventCommand);
            if (!validationResult.IsFail)
            {
                Event oldEvent = await _eventRepository.GetEventAsync(updateEventCommand.UserId,
                    updateEventCommand.StartEvent, updateEventCommand.StartEvent);
                oldEvent.SetName(updateEventCommand.Name);
                oldEvent.SetDescription(updateEventCommand.Description);
                oldEvent.SetDateEvent(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
