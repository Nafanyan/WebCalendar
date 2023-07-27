namespace Application.Entities
{
    public class Event
    {
        public long UserId { get; init; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartEvent { get; set; }
        public DateTime EndEvent { get; set; }

        public Event( long userId, string name, string description, DateTime startEvent, DateTime endEvent )
        {
            UserId = userId;
            Name = name;
            Description = description;
            StartEvent = startEvent;
            EndEvent = endEvent;
        }

        public Event()
        {

        }

        public void SetName( string name )
        {
            Name = name;
        }
        public void SetDescription( string description )
        {
            Description = description;
        }
        public void SetDateEvent( DateTime startEvent, DateTime endEvent )
        {
            StartEvent = startEvent;
            EndEvent = endEvent;
        }
    }
}
