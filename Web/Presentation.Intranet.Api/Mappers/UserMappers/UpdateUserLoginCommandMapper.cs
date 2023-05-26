using Application.Users.Commands.UpdateUserLogin;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class UpdateUserLoginCommandMapper
    {
        public static UpdateUserLoginCommand Map(this UpdateUserLoginDto updateUserLoginDto)
        {
            return new UpdateUserLoginCommand
            {
                Id = updateUserLoginDto.Id,
                Login = updateUserLoginDto.Login,
                PasswordHash = updateUserLoginDto.PasswordHash
            };
        }
    }
}
