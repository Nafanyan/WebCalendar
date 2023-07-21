namespace Presentation.Intranet.Api.Dtos.UserDtos
{
    public class UpdateUserLoginDto
    {
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
