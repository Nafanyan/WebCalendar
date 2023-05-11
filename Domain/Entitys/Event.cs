namespace Domain.Entitys
{
    public class Event
    {
        public long Id { get; private set; }
        public string EventRecord { get; private set; }
        public string Description { get; private set; }
        public EventPeriod EventPeriod { get; private set; }

        public Event(string record, string description, EventPeriod eventPeriod)
        {
            EventRecord = record;
            Description = description;
            EventPeriod = eventPeriod;
        }

        public void SetRecord(string record)
        {
            EventRecord = record;
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
