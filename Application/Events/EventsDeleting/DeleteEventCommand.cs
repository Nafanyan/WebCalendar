namespace Application.Events.EventsDeleting
{
    public class DeleteEventCommand
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
