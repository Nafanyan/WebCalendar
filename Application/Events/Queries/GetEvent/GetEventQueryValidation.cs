using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Queries.GetEvent
{
    public class GetEventQueryValidation : IAsyncValidator<GetEventQuery>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventQueryValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> Validation(GetEventQuery query)
        {
            if (query.StartEvent == null)
            {
                return ValidationResult.Fail("The start date cannot be empty/cannot be null");
            }

            if (query.EndEvent == null)
            {
                return ValidationResult.Fail("The end date cannot be empty/cannot be null");
            }

            if (query.StartEvent > query.EndEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }

            EventPeriod eventPeriod = new EventPeriod(query.StartEvent, query.EndEvent);
            if (!await _eventRepository.Contains(query.UserId, eventPeriod))
            {
                return ValidationResult.Fail("An event with such a time does not exist");
            }

            return ValidationResult.Ok();
        }
    }
}
