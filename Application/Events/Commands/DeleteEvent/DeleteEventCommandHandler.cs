using Application.Events.Commands.CreateEvent;
using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncValidator<DeleteEventCommand> _deleteEventCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventCommandHandler(
            IEventRepository eventRepository, 
            IAsyncValidator<DeleteEventCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _deleteEventCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorizationCommandResult> HandleAsync(DeleteEventCommand deleteEventCommand)
        {
            ValidationResult validationResult = await _deleteEventCommandValidator.ValidationAsync(deleteEventCommand);
            if (!validationResult.IsFail)
            {
                Event foundEvent = await _eventRepository.GetEventAsync(deleteEventCommand.UserId,
                    deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
                await _eventRepository.DeleteAsync(foundEvent);
                await _unitOfWork.CommitAsync();
            }
            return new AuthorizationCommandResult(validationResult);
        }
    }
}
