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

        public string Validation(GetEventQuery query)
        {
            if (query.StartEvent == null)
            {
                return "The start date cannot be empty/cannot be null";
            }

            if (query.EndEvent == null)
            {
                return "The end date cannot be empty/cannot be null";
            }

            if (query.StartEvent > query.EndEvent)
            {
                return "The start date cannot be later than the end date";
            }

            EventPeriod eventPeriod = new EventPeriod(query.StartEvent, query.EndEvent);
            if (_eventRepository.GetEvent(query.UserId, eventPeriod).Result == null)
            {
                return "An event with such a time does not exist";
            }

            return "Ok";
        }

    }
}
