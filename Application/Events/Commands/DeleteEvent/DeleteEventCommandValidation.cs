
using Application.Events.Queries.GetEvent;
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

        public string Validation(DeleteEventCommand deleteEventCommand)
        {
            if (deleteEventCommand.StartEvent == null)
            {
                return "The start date cannot be empty/cannot be null";
            }

            if (deleteEventCommand.EndEvent == null)
            {
                return "The end date cannot be empty/cannot be null";
            }

            if (deleteEventCommand.StartEvent > deleteEventCommand.EndEvent)
            {
                return "The start date cannot be later than the end date";
            }

            EventPeriod eventPeriod = new EventPeriod(deleteEventCommand.StartEvent, deleteEventCommand.EndEvent);
            if (_eventRepository.GetEvent(deleteEventCommand.UserId, eventPeriod).Result == null)
            {
                return "An event with such a time does not exist";
            }

            return "Ok";
        }
    }
}
