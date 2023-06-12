namespace Presentation.Intranet.Api.Dtos.EventRequest
{
    public class CreateEventDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}
