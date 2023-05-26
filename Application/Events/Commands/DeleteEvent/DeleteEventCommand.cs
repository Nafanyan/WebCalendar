namespace Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand
    {
        public long UserId { get; init; }
        public string StartEvent { get; init; }
        public string EndEvent { get; init; }
    }
}
