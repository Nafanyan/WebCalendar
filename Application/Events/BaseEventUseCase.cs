using Domain.Repositories;
using WebCalendar.Domain.Validation.EventValidation;

namespace WebCalendar.Application.Events
{
    public abstract class BaseEventUseCase
    {
        protected readonly IEventRepository _eventRepository;
        protected readonly EventValidation _validationEvent;

        public BaseEventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _validationEvent = new EventValidation(_eventRepository);
        }
    }
}
