using Application.Validation;
using Domain.Repositories;

namespace Application.Events
{
    public abstract class BaseEventHandler
    {
        protected readonly IEventRepository EventRepository;
        protected readonly EventValidation ValidationEvent;

        public BaseEventHandler(IEventRepository eventRepository)
        {
            EventRepository = eventRepository;
            ValidationEvent = new EventValidation(EventRepository);
        }
    }
}
