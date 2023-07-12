using Application.Users.Commands.DeleteUser;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class DeleteUserCommandMapper
    {
        public static DeleteUserCommand Map(this DeleteUserDto deleteUserDto)
        {
            return new DeleteUserCommand
            {
                Id = deleteUserDto.Id,
                PasswordHash = deleteUserDto.PasswordHash
            };
        }
    }
}
