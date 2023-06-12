using Application.Events.Commands.DeleteEvent;
using Presentation.Intranet.Api.Dtos.EventRequest;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class DeleteEventCommandMapper
    {
        public static DeleteEventCommand Map(this DeleteEventDto deleteEventRequest, long userId)
        {
            return new DeleteEventCommand
            {
                UserId = userId,
                StartEvent = deleteEventRequest.StartEvent,
                EndEvent = deleteEventRequest.EndEvent
            };
        }
    }
}
