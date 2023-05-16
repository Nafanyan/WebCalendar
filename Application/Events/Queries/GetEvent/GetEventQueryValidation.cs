using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Queries.GetEvent
{
    public class GetEventQueryValidation : IValidator<GetEventQuery>, IAsyncValidator<GetEventQuery>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventQueryValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public ValidationResult Validation(GetEventQuery query)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            if (query.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            if (query.EndEvent == null)
            {
                error = "The end date cannot be empty/cannot be null";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            if (query.StartEvent > query.EndEvent)
            {
                error = "The start date cannot be later than the end date";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            validationResult = AsyncValidation(query).Result;
            if (validationResult.IsFail)
            {
                return validationResult;
            }

            return new ValidationResult(false, error);
        }
        public async Task<ValidationResult> AsyncValidation(GetEventQuery query)
        {
            string error = "No errors";
            EventPeriod eventPeriod = new EventPeriod(query.StartEvent, query.EndEvent);

            if (await _eventRepository.GetEvent(query.UserId, eventPeriod) == null)
            {
                error = "An event with such a time does not exist";
                return new ValidationResult(true, error);
            }
            return new ValidationResult(false, error);
        }
    }
}
