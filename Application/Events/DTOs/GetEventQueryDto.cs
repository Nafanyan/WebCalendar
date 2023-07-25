namespace Application.Events.DTOs
{
    public class GetEventQueryDto
    {
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
