namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQuery
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
