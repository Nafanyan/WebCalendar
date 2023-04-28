namespace WebCalendar.Application.Events.Commands
{
    public class AddEventCommand
    {
        public string Record { get; init; }
        public string Description { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
