using Domain.Entities;
using Domain.Repositories;

namespace Application.Validation.EventValidation
{
    public class EventValidation
    {
        private readonly IEventRepository _eventRepository;

        public EventValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void ValueNotFound(long userId, EventPeriod eventPeriod)
        {
            if (_eventRepository.GetEvent(userId, eventPeriod) == null)
            {
                throw new EventException("An event with such a time does not exist");
            }
        }
        public void DateСorrectness(DateTime startEvent, DateTime endEvent)
        {
            if (startEvent > endEvent)
            {
                throw new EventException("The start date cannot be later than the end date");
            }
        }
        public void NameNull(string name)
        {
            if (name == null)
            {
                throw new EventException("The name of event cannot be empty/cannot be null");
            }
        }
        public void DateNull(DateTime startEvent, DateTime endEvent)
        {
            if (startEvent == null)
            {
                throw new EventException("The start date cannot be empty/cannot be null");
            }
            
            if (endEvent == null)
            {
                throw new EventException("The end date cannot be empty/cannot be null");
            }
        }
    }
}
