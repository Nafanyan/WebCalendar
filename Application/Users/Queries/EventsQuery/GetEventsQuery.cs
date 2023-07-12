namespace Application.Users.Queries.EventsQuery
{
    public class GetEventsQuery
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
