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
                return ValidationResult.Fail( "Дата начала не может быть позже даты окончания" );
            }

            if( query.StartEvent.ToShortDateString() != query.EndEvent.ToShortDateString() )
            {
                return ValidationResult.Fail( "Событие должно произойти в течение одного дня" );
            }

            if( !await _eventRepository.ContainsAsync( query.UserId, query.StartEvent, query.EndEvent ) )
            {
                return ValidationResult.Fail( "Для текущего пользователя нет события с этим временем" );
            }

            return ValidationResult.Ok();
        }
    }
}
