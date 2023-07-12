namespace Application.Events.Commands.DeleteEvent
{
    public class EventDeleteCommand
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
