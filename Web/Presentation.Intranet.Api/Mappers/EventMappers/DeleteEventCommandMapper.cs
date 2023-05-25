using Application.Events.Commands.DeleteEvent;
using Presentation.Intranet.Api.Dtos.EventDtos;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class DeleteEventCommandMapper
    {
        public static DeleteEventCommand Map(this DeleteEventDto deleteEventDto)
        {
            return new DeleteEventCommand
            {
                UserId = deleteEventDto.UserId,
                StartEvent = deleteEventDto.StartEvent.MapStringToDate(),
                EndEvent = deleteEventDto.EndEvent.MapStringToDate()
            };
        }
    }
}
