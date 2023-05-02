namespace WebCalendar.Domain.Events
{
    public class Event
    {
        public long Id { get; private set; }
        public string Record { get; private set; }
        public string? Description { get; private set; }
        public DateTime StartEvent { get; private set; }
        public DateTime EndEvent { get; private set; }

        public Event( string record, string description, DateTime startEvent, DateTime endEvent)
        {
            Record = record;
            Description = description;
            StartEvent = startEvent;
            EndEvent = endEvent;
        }
        public void UpdateRecord(string record)
        {
            Record = record;
        }
        public void UpdateDescription(string description)
        {
            Description = description;
        }
        public void UpdateDateEvent(DateTime startEvent, DateTime endEvent)
        {
            StartEvent = startEvent;
            EndEvent = endEvent;
        }

    }
}
