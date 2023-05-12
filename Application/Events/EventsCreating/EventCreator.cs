﻿using WebCalendar.Domain.Events;

namespace WebCalendar.Application.Events.EventsCreating
{
    public interface IEventCreator
    {
        void Create(AddEventCommand addEventCommand);
    }
    public class EventCreator : BaseEventUsCase, IEventCreator
    {
        public EventCreator(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public void Create(AddEventCommand addEventCommand)
        {
            ValidationCheck(addEventCommand);

            Event e = new Event(addEventCommand.Record, addEventCommand.Description, 
                addEventCommand.StartEvent, addEventCommand.EndEvent);

            _eventRepository.Add(e);
        }

        private void ValidationCheck(AddEventCommand addEventCommand)
        {
            _validationEvent.NameNull(addEventCommand.Record);
            _validationEvent.DateNull(addEventCommand.StartEvent, addEventCommand.EndEvent);
            _validationEvent.DateСorrectness(addEventCommand.StartEvent, addEventCommand.EndEvent);
        }
    }
}
