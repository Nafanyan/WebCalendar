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
            string error;
            if (query.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            if (query.EndEvent == null)
            {
                error = "The end date cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            if (query.StartEvent > query.EndEvent)
            {
                error = "The start date cannot be later than the end date";
                return ValidationResult.Fail(error);
            }

            EventPeriod eventPeriod = new EventPeriod(query.StartEvent, query.EndEvent);
            if (!await _eventRepository.Contains(query.UserId, eventPeriod))
            {
                error = "An event with such a time does not exist";
                return ValidationResult.Fail(error);
            }

            return ValidationResult.Ok();
        }
    }
}
