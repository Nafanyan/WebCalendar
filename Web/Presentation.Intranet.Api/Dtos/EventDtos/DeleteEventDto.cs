namespace Presentation.Intranet.Api.Dtos.EventDtos
{
    public class DeleteEventDto
    {
        public long UserId { get; init; }
        public string StartEvent { get; init; }
        public string EndEvent { get; init; }
    }
}
