using Application.Users.Commands.DeleteUser;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class DeleteUserCommandMapper
    {
        public static UserDeleteCommand Map(this DeleteUserDto deleteUserDto)
        {
            return new UserDeleteCommand
            {
                Id = deleteUserDto.Id,
                PasswordHash = deleteUserDto.PasswordHash
            };
        }
    }
}
