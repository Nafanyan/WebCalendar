using Application.Events.Commands.UpdateEvent;
using Presentation.Intranet.Api.Dtos.EventDtos;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class UpdateEventCommandMapper
    {
        public static UpdateEventCommand Map(this UpdateEventDto updateEventDto)
        {
            return new UpdateEventCommand
            {
                UserId = updateEventDto.UserId,
                Name = updateEventDto.Name,
                Description = updateEventDto.Description,
                StartEvent = updateEventDto.StartEvent.MapStringToDate(),
                EndEvent = updateEventDto.EndEvent.MapStringToDate()
            };
        }
    }
}
