namespace Application.Events.EventsReceiving
{
    public class ReceiveEventCommand
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
