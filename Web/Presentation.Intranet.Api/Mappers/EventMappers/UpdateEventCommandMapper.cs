﻿using Application.Events.Commands.UpdateEvent;
using Presentation.Intranet.Api.Dtos.EventRequest;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class UpdateEventCommandMapper
    {
        public static UpdateEventCommand Map(this UpdateEventRequest updateEventRequest, long userId)
        {
            return new UpdateEventCommand
            {
                UserId = userId,
                Name = updateEventRequest.Name,
                Description = updateEventRequest.Description,
                StartEvent = updateEventRequest.StartEvent.MapStringToDate(),
                EndEvent = updateEventRequest.EndEvent.MapStringToDate()
            };
        }
    }
}
