using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandValidation : IValidation<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public string Validation(DeleteEventCommand command)
        {
            if (command.StartEvent == null)
            {
                return "The start date cannot be empty/cannot be null";
            }

            if (command.EndEvent == null)
            {
                return "The end date cannot be empty/cannot be null";
            }

            if (command.StartEvent > command.EndEvent)
            {
                return "The start date cannot be later than the end date";
            }

            EventPeriod eventPeriod = new EventPeriod(command.StartEvent, command.EndEvent);
            if (_eventRepository.GetEvent(command.UserId, eventPeriod).Result == null)
            {
                return "An event with such a time does not exist";
            }

            return "Ok";
        }
    }
}
