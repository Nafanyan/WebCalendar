using WebCalendar.Domain.Events;

namespace WebCalendar.Application.Events.EventsDeleting
{
    public interface IEventDeletor
    {
        public void Delete(long id);
    }
    public class EventDeleter : BaseEventUsCase, IEventDeletor
    {
        public EventDeleter(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Delete(long id)
        {
            ValidationCheck(id);
            _eventRepository.Delete(id);
        }

        private void ValidationCheck(long id)
        {
            _validationEvent.CheckingContentInRepository(id);
        }
    }
}
