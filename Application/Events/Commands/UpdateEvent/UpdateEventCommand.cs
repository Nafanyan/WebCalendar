namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand
    {
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string StartEvent { get; init; }
        public string EndEvent { get; init; }
    }
}
