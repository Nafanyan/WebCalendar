namespace Domain.Entities
{
    public class EventPeriod
    {
        public DateTime StartEvent { get; set; }
        public DateTime EndEvent { get; set; }

        public EventPeriod(DateTime startEvent, DateTime endEvent)
        {
            if (startEvent.ToShortDateString != endEvent.ToShortDateString)
            {
                new ArgumentException("The event must occur within one day");
            }
            StartEvent = startEvent;
            EndEvent = endEvent;
        }
    }
}
