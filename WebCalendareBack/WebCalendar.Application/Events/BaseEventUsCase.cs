using WebCalendar.Domain.Events;
using WebCalendar.Domain.Validation.EventValidation;

namespace WebCalendar.Application.Events
{
    public abstract class BaseEventUsCase
    {
        protected readonly IEventRepository _eventRepository;
        protected readonly ValidationEvent _validationEvent;

        public BaseEventUsCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _validationEvent = new ValidationEvent(_eventRepository);
        }
    }
}
