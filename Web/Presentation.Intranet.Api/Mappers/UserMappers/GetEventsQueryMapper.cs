using Application.Users.Queries.GetEvents;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class GetEventsQueryMapper
    {
        public static GetEventsQuery Map(this GetEventsDto getEventsDto)
        {
            return new GetEventsQuery
            {
                UserId = getEventsDto.UserId
            };
        }
    }
}
