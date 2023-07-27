using Application.Validation;
using Application.Repositories;

namespace Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandValidator : IAsyncValidator<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandValidator( IEventRepository eventRepository )
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> ValidationAsync( DeleteEventCommand command )
        {
            if( command.StartEvent.ToShortDateString() != command.EndEvent.ToShortDateString() )
            {
                return ValidationResult.Fail( "Событие должно произойти в течение одного дня" );
            }

            if( command.StartEvent > command.EndEvent )
            {
                return ValidationResult.Fail( "Дата начала не может быть позже даты окончания" );
            }

            if( await _eventRepository.GetEventAsync( command.UserId, command.StartEvent, command.EndEvent ) == null )
            {
                return ValidationResult.Fail( "Такого события не существует" );
            }

            return ValidationResult.Ok();
        }
    }
}
