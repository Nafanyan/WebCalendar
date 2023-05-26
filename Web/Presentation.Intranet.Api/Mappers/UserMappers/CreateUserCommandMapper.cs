using Application.Users.Commands.CreateUser;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class CreateUserCommandMapper
    {
        public static CreateUserCommand Map(this CreateUserDto createUserDto)
        {
            return new CreateUserCommand
            {
                Login = createUserDto.Login,
                PasswordHash = createUserDto.PasswordHash
            };
        }
    }
}
