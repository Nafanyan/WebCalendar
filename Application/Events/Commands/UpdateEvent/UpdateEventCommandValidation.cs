using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidation : IValidation<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        public UpdateEventCommandValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public string Validation(UpdateEventCommand updateEventCommand)
        {
            if (updateEventCommand.StartEvent == null)
            {
                return "The start date cannot be empty/cannot be null";
            }

            if (updateEventCommand.EndEvent == null)
            {
                return "The end date cannot be empty/cannot be null";
            }

            if (updateEventCommand.StartEvent > updateEventCommand.EndEvent)
            {
                return "The start date cannot be later than the end date";
            }

            EventPeriod eventPeriod = new EventPeriod(updateEventCommand.StartEvent, updateEventCommand.EndEvent);
            if (_eventRepository.GetEvent(updateEventCommand.UserId, eventPeriod).Result == null)
            {
                return "An event with such a time does not exist";
            }

            return "Ok";
        }
    }
}
