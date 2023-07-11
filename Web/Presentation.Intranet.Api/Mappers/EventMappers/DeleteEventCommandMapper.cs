using Application.Events.Commands.DeleteEvent;
using Presentation.Intranet.Api.Dtos.EventRequest;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class DeleteEventCommandMapper
    {
        public static EventDeleteCommand Map(this DeleteEventDto deleteEventRequest, long userId)
        {
            return new EventDeleteCommand
            {
                UserId = userId,
                StartEvent = deleteEventRequest.StartEvent,
                EndEvent = deleteEventRequest.EndEvent
            };
        }
    }
}
