namespace Application.Users.Queries.EventsQuery
{
    public class EventsQuery
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
