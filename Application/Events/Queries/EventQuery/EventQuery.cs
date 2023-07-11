namespace Application.Events.Queries.EventQuery
{
    public class EventQuery
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
