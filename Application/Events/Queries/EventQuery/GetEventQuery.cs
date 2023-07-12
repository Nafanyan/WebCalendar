namespace Application.Events.Queries.EventQuery
{
    public class GetEventQuery
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
