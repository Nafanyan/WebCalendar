namespace WebCalendar.Application.Events.EventsUpdating
{
    public class UpdateEventCommand
    {
        public long Id { get; init; }
        public string Record { get; init; }
        public string Description { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
