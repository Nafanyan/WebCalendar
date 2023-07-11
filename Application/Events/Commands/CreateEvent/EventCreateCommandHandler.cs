using Application.Result;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Application.Validation;
using Domain.UnitOfWork;

namespace Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : ICommandHandler<EventCreateCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncValidator<EventCreateCommand> _createEventCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventCommandHandler(
            IEventRepository eventRepository, 
            IAsyncValidator<EventCreateCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _createEventCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(EventCreateCommand createEventCommand)
        {
            ValidationResult validationResult = await _createEventCommandValidator.ValidationAsync(createEventCommand);
            if (!validationResult.IsFail)
            {

                Event newEvent = new Event(createEventCommand.UserId, createEventCommand.Name, createEventCommand.Description,
                    createEventCommand.StartEvent, createEventCommand.EndEvent);
                await _eventRepository.AddAsync(newEvent);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
