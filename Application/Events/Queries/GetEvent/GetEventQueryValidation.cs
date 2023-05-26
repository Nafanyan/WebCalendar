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

        public async Task<ValidationResult> ValidationAsync(GetEventQuery query)
        {
            if (query.StartEvent == null)
            {
                return ValidationResult.Fail("The start date cannot be empty/cannot be null");
            }

            if (query.EndEvent == null)
            {
                return ValidationResult.Fail("The end date cannot be empty/cannot be null");
            }

            DateTime startEvent;
            if (!DateTime.TryParse(query.StartEvent, out startEvent))
            {
                return ValidationResult.Fail("Error in writing the start date of the event");
            }

            DateTime endEvent;
            if (!DateTime.TryParse(query.EndEvent, out endEvent))
            {
                return ValidationResult.Fail("Error in writing the end date of the event");
            }

            if (startEvent > endEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }

            if (startEvent.ToShortDateString() != endEvent.ToShortDateString())
            {
                return ValidationResult.Fail("The event must occur within one day");
            }

            if (!await _eventRepository.ContainsAsync(query.UserId, startEvent, endEvent))
            {
                return ValidationResult.Fail("There is no event with this time for the current user");
            }

            return ValidationResult.Ok();
        }
    }
}
