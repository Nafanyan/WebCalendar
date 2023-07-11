using Application.Events.Commands.UpdateEvent;
using Presentation.Intranet.Api.Dtos.EventRequest;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class UpdateEventCommandMapper
    {
        public static EventUpdateCommand Map(this UpdateEventDto updateEventRequest, long userId)
        {
            return new EventUpdateCommand
            {
                UserId = userId,
                Name = updateEventRequest.Name,
                Description = updateEventRequest.Description,
                StartEvent = updateEventRequest.StartEvent,
                EndEvent = updateEventRequest.EndEvent
            };
        }
    }
}
