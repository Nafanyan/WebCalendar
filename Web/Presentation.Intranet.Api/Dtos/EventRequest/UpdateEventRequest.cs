﻿namespace Presentation.Intranet.Api.Dtos.EventRequest
{
    public class UpdateEventRequest
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string StartEvent { get; init; }
        public string EndEvent { get; init; }
    }
}
