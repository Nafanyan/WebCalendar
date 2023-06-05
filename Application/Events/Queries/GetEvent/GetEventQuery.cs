namespace Application.Events.Queries.GetEvent
{
    public class GetEventQuery
    {
        public long UserId { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
