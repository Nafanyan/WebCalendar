namespace Application.Events.Queries.GetEvent
{
    public class GetEventQuery
    {
        public long UserId { get; init; }
        public string StartEvent { get; init; }
        public string EndEvent { get; init; }
    }
}
