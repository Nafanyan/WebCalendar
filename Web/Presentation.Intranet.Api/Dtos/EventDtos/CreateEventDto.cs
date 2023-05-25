namespace Presentation.Intranet.Api.Dtos.EventDtos
{
    public class CreateEventDto
    {
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string StartEvent { get; init; }
        public string EndEvent { get; init; }
    }
}
