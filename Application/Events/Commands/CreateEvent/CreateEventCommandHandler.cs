using Application.Result;
using Application.Interfaces;
using Domain.Entities;
using Application.Repositories;
using Application.Validation;

namespace Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncValidator<CreateEventCommand> _createEventCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventCommandHandler(
            IEventRepository eventRepository,
            IAsyncValidator<CreateEventCommand> validator,
            IUnitOfWork unitOfWork )
        {
            _eventRepository = eventRepository;
            _createEventCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync( CreateEventCommand createEventCommand )
        {
            ValidationResult validationResult = await _createEventCommandValidator.ValidationAsync( createEventCommand );
            if( !validationResult.IsFail )
            {
                Event newEvent = new Event( createEventCommand.UserId, createEventCommand.Name, createEventCommand.Description,
                    createEventCommand.StartEvent, createEventCommand.EndEvent );
                _eventRepository.Add( newEvent );
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult( validationResult );
        }
    }
}
