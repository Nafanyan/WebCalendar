using Application.Users.Commands.UpdateUserLogin;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class UpdateUserLoginCommandMapper
    {
        public static UserLoginUpdateCommand Map(this UpdateUserLoginDto updateUserLoginDto)
        {
            return new UserLoginUpdateCommand
            {
                Id = updateUserLoginDto.Id,
                Login = updateUserLoginDto.Login,
                PasswordHash = updateUserLoginDto.PasswordHash
            };
        }
    }
}
