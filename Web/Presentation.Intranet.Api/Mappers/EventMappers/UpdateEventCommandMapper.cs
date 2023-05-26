using Application.Events.Commands.UpdateEvent;
using Presentation.Intranet.Api.Dtos.EventDtos;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class UpdateEventCommandMapper
    {
        public static UpdateEventCommand Map(this UpdateEventDto updateEventDto, long userId)
        {
            return new UpdateEventCommand
            {
                UserId = userId,
                Name = updateEventDto.Name,
                Description = updateEventDto.Description,
                StartEvent = updateEventDto.StartEvent.MapStringToDate(),
                EndEvent = updateEventDto.EndEvent.MapStringToDate()
            };
        }
    }
}
