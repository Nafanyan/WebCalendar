using Application.Validation;
using Application.Repositories;

namespace Application.Events.Queries.GetEvent
{
    public class GetEventQueryValidator : IAsyncValidator<GetEventQuery>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventQueryValidator( IEventRepository eventRepository )
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> ValidationAsync( GetEventQuery query )
        {
            if( query.StartEvent > query.EndEvent )
            {
                return ValidationResult.Fail( "The start date cannot be later than the end date" );
            }

            if( query.StartEvent.ToShortDateString() != query.EndEvent.ToShortDateString() )
            {
                return ValidationResult.Fail( "The event must occur within one day" );
            }

            if( !await _eventRepository.ContainsAsync( query.UserId, query.StartEvent, query.EndEvent ) )
            {
                return ValidationResult.Fail( "There is no event with this time for the current user" );
            }

            return ValidationResult.Ok();
        }
    }
}
