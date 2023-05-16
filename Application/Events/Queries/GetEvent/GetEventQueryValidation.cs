using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Queries.GetEvent
{
    public class GetEventQueryValidation : IValidation<GetEventQuery>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventQueryValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public ValidationResult Validation(GetEventQuery query)
        {
            string error = "No errors";
            if (query.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (query.EndEvent == null)
            {
                error = "The end date cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (query.StartEvent > query.EndEvent)
            {
                error = "The start date cannot be later than the end date";
                return new ValidationResult(true, error);
            }

            EventPeriod eventPeriod = new EventPeriod(query.StartEvent, query.EndEvent);
            if (_eventRepository.GetEvent(query.UserId, eventPeriod).Result == null)
            {
                error = "An event with such a time does not exist";
                return new ValidationResult(true, error);
            }

            return new ValidationResult(false, error);
        }

    }
}
