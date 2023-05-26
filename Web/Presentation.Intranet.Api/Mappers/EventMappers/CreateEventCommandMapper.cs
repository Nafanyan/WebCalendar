using Application.Events.Commands.CreateEvent;
using Presentation.Intranet.Api.Dtos.EventRequest;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class CreateEventCommandMapper
    {
        public static CreateEventCommand Map(this CreateEventRequest createEventRequest, long userId)
        {
            return new CreateEventCommand
            {
                UserId = userId,
                Name = createEventRequest.Name,
                Description = createEventRequest.Description,
                StartEvent = createEventRequest.StartEvent.MapStringToDate(),
                EndEvent = createEventRequest.EndEvent.MapStringToDate()
            };
        }
    }
}
