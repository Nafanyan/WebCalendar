using WebCalendar.Domain.Events;

namespace WebCalendar.Domain.Validation.EventValidation
{
    public class ValidationEvent
    {
        private IEventRepository _eventRepository;
        public ValidationEvent(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void CheckingContentInRepository(long id)
        {
            if (_eventRepository.GetById(id) == null)
            {
                throw new EventException("There is no record with this id");
            }
        }

        public void CheckingTheDate(DateTime startEvent, DateTime endEvent)
        {
            if (startEvent > endEvent)
            {
                throw new EventException("The start date cannot be later than the end date");
            }
        }

        public void CheckingTheRecord(string record)
        {
            if (record == null)
            {
                throw new EventException("The record cannot be empty/cannot be null");
            }
        }

        public void CheckingDateForNull(DateTime startEvent, DateTime endEvent)
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
