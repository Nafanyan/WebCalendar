using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Queries.GetEvent
{
    public class GetEventQueryValidator : IAsyncValidator<GetEventQuery>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventQueryValidator(IEventRepository eventRepository)
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


            if (query.StartEvent.ToShortDateString() != query.EndEvent.ToShortDateString())
            {
                return ValidationResult.Fail("The event must occur within one day");
            }

            if (query.StartEvent > query.EndEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }

            if (await _eventRepository.ContainsAsync(query.UserId, query.StartEvent, query.EndEvent))
            {
                return ValidationResult.Fail("This event is superimposed on the existing event in time");
            }

            return ValidationResult.Ok();
        }
    }
}
