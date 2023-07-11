using Application.Users.Commands.CreateUser;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class CreateUserCommandMapper
    {
        public static UserCreateCommand Map(this CreateUserDto createUserDto)
        {
            return new UserCreateCommand
            {
                Login = createUserDto.Login,
                PasswordHash = createUserDto.PasswordHash
            };
        }
    }
}
