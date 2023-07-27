using Application.Validation;
using Application.Entities;
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
                return ValidationResult.Fail( "The name of event cannot be empty/cannot be null" );
            }

            if( command.Name.Length > 100 )
            {
                return ValidationResult.Fail( "The name of event cannot be more than 100 characters" );
            }

            if( command.Description.Length > 300 )
            {
                return ValidationResult.Fail( "The description of event cannot be more than 300 characters" );
            }

            if( command.StartEvent.ToShortDateString() != command.EndEvent.ToShortDateString() )
            {
                return ValidationResult.Fail( "The event must occur within one day" );
            }

            if( command.StartEvent > command.EndEvent )
            {
                return ValidationResult.Fail( "The start date cannot be later than the end date" );
            }

            if( await _eventRepository.GetEventAsync( command.UserId, command.StartEvent, command.EndEvent ) == null )
            {
                return ValidationResult.Fail( "There is no such event" );
            }

            return ValidationResult.Ok();
        }
    }
}
