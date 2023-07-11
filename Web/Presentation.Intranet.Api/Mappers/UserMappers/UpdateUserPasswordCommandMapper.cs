using Application.Users.Commands.UpdateUserPassword;
using Presentation.Intranet.Api.Dtos.UserDtos;

namespace Presentation.Intranet.Api.Mappers.UserMappers
{
    public static class UpdateUserPasswordCommandMapper
    {
        public static UserPasswordUpdateCommand Map(this UpdateUserPasswordDto updateUserPasswordDto)
        {
            return new UserPasswordUpdateCommand
            {
                Id = updateUserPasswordDto.Id,
                OldPasswordHash = updateUserPasswordDto.OldPasswordHash,
                NewPasswordHash = updateUserPasswordDto.NewPasswordHash
            };
        }
    }
}
