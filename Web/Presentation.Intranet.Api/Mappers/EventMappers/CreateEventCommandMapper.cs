using Application.Events.Commands.CreateEvent;
using Presentation.Intranet.Api.Dtos.EventDtos;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class CreateEventCommandMapper
    {
        public static CreateEventCommand Map(this CreateEventDto createEventDto)
        {
            return new CreateEventCommand
            {
                UserId = createEventDto.UserId,
                Name = createEventDto.Name,
                Description = createEventDto.Description,
                StartEvent = createEventDto.StartEvent.MapStringToDate(),
                EndEvent = createEventDto.EndEvent.MapStringToDate()
            };
        }
    }
}
