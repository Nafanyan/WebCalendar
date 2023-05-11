﻿namespace Domain.Entitys
{
    public class Event
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EventPeriod EventPeriod { get; private set; }

        public Event(string name, string description, EventPeriod eventPeriod)
        {
            Name = name;
            Description = description;
            EventPeriod = eventPeriod;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void SetDateEvent(EventPeriod eventPeriod)
        {
            EventPeriod = eventPeriod;
        }
    }

    public class EventPeriod
    {
        public DateTime StartEvent { get; set; }
        public DateTime EndEvent { get; set; }
    }
}