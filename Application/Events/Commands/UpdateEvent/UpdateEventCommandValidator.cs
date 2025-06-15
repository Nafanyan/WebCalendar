using Application.Validation;
using Application.Repositories;

namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : IAsyncValidator<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandValidator( IEventRepository eventRepository )
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> ValidationAsync( UpdateEventCommand command )
        {
            if( command.Name == null || command.Name == String.Empty )
            {
                return ValidationResult.Fail( "Имя события не может быть пустым" );
            }

            if( command.Name.Length > 100 )
            {
                return ValidationResult.Fail( "Имя события не может быть больше чем 100 символов" );
            }

            if( command.Description.Length > 300 )
            {
                return ValidationResult.Fail( "Описание события не может быть больше чем 300 символов" );
            }

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
