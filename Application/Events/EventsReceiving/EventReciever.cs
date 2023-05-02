using WebCalendar.Domain.Events;
using WebCalendar.Domain.Validation.UserValidation;

namespace WebCalendar.Application.Events.EventsReciever
{
    public interface IEventReciever
    {
        public Event GetEvent(long id);
    }
    public class EventReciever : BaseEventUsCase, IEventReciever
    {
        public EventReciever(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public Event GetEvent(long id)
        {
            ValidationCheck(id);
            return _eventRepository.GetById(id);
        }

        private void ValidationCheck(long id)
        {
            _validationEvent.CheckingContentInRepository(id);
        }
    }
}
