﻿namespace Application.Events.Commands.UpdateEvent
{
    public class EventUpdateCommand
    {
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime StartEvent { get; init; }
        public DateTime EndEvent { get; init; }
    }
}