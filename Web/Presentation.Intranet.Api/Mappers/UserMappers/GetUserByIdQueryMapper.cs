using Application.Users.Queries.GetUserById;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class GetUserByIdQueryMapper
    {
        public static GetUserByIdQuery Map(this GetUserByIdDto getUserByIdDto)
        {
            return new GetUserByIdQuery
            {
                Id = getUserByIdDto.Id
            };
        }
    }
}
